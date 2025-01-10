using System.Collections.Concurrent;
using CodeClash.API.Extensions;
using CodeClash.Application.Extensions;
using CodeClash.Application.Services;
using CodeClash.Core;
using CodeClash.Core.Models.DTOs;
using CodeClash.Core.Requests.RoomsRequests;
using CodeClash.Core.Requests.SolutionsRequests;
using CodeClash.Persistence.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace CodeClash.API.Hubs;

[Authorize]
[EnableCors("CorsPolicy")]
public class RoomHub(
    RoomService roomService,
    CompetitionService competitionService,
    UserService userService,
    TestUserSolutionService testUserSolutionService) : Hub
{
    private static readonly ConcurrentDictionary<Guid, CancellationTokenSource> CancellationTokenDict = new();

    public override Task OnConnectedAsync()
    {
        var userId = new Guid(Context.UserIdentifier!);
        var roomId = userService.GetUserRoomId(userId).Result;
        if (roomId.HasValue)
            AddUserToGroup(roomId.Value).Wait();
        return base.OnConnectedAsync();
    }

    public async Task<ApiResponse<RoomDTO>> CreateRoom(CreateRoomRequest request)
    {
        var userId = Context.User.GetUserIdFromAccessToken();
        var roomResult = await roomService.CreateRoom(request.RoomName, request.Time, request.IssueId, userId);
        if (roomResult.IsFailure)
            return new ApiResponse<RoomDTO>(false, null, roomResult.Error);

        var room = roomResult.Value;
        await AddUserToGroup(room.Id);
        return new ApiResponse<RoomDTO>(true, room.GetRoomDtoFromRoom(), null);
    }

    public async Task<ApiResponse<string>> JoinRoom(Guid roomId)
    {
        var userId = Context.User.GetUserIdFromAccessToken();
        var joinedRoomResult = await roomService.JoinRoom(roomId, userId);
        if (joinedRoomResult.IsFailure)
            return new ApiResponse<string>(false, null, joinedRoomResult.Error);

        await SendMessageToAllUsersInGroup(joinedRoomResult.Value.Id, joinedRoomResult.Value.GetRoomDtoFromRoom(),
            "UserJoined");
        await AddUserToGroup(joinedRoomResult.Value.Id);
        return new ApiResponse<string>(true, "Joined to room successfully.", null);
    }

    public async Task<ApiResponse<string>> QuitRoom()
    {
        var userId = Context.User.GetUserIdFromAccessToken();
        var roomId = await userService.GetUserRoomId(userId);
        if (!roomId.HasValue)
            return new ApiResponse<string>(false, null, "User is not in room.");
        var leftRoomResult = await roomService.QuitRoom(userId, roomId.Value, Context, Groups, CancellationTokenDict);
        if (leftRoomResult.IsFailure)
            return new ApiResponse<string>(false, null, leftRoomResult.Error);

        await SendMessageToAllUsersInGroup(roomId.Value, leftRoomResult.Value?.GetRoomDtoFromRoom(), "UserLeave");
        return new ApiResponse<string>(true, "Quited from room successfully.", null);
    }

    public async Task<ApiResponse<string>> StartCompetition(RoomDTO roomDto)
    {
        var roomId = new Guid(roomDto.Id);
        var duration = roomDto.Time;
        var userId = Context.User.GetUserIdFromAccessToken();
        var userIsAdmin = await competitionService.GetUserStatus(userId);
        if (userIsAdmin.IsFailure)
            return new ApiResponse<string>(false, null, userIsAdmin.Error);
        if (!userIsAdmin.Value)
            return new ApiResponse<string>(false, null, "User is not admin");

        var roomStatus = await competitionService.GetRoomStatus(roomId);
        if (roomStatus.IsFailure)
            return new ApiResponse<string>(false, null, roomStatus.Error);
        if (roomStatus.Value is RoomStatus.CompetitionInProgress)
            return new ApiResponse<string>(false, null, "Competition in progress");

        var roomEntityResult = await competitionService.UpdateRoomStatus(roomId, RoomStatus.CompetitionInProgress);
        if (roomEntityResult.IsFailure)
            return new ApiResponse<string>(false, null, roomEntityResult.Error);

        await SendMessageToAllUsersInGroup(roomId, $"/problem/{roomEntityResult.Value.IssueId}", "CompetitionStarted");
        var cts = new CancellationTokenSource();
        CancellationTokenDict[roomId] = cts;
        _ = competitionService.SyncTimers(Clients.Group(roomId.ToString()), duration, roomId, CancellationTokenDict);

        return new ApiResponse<string>(true, "Competition is started", null);
    }

    public async Task<ApiResponse<string>> CheckSolution(CheckSolutionRequest checkSolutionRequest)
    {
        var userId = Context.User.GetUserIdFromAccessToken();
        var roomId = await userService.GetUserRoomId(userId);
        if (!roomId.HasValue)
            return new ApiResponse<string>(false, null, "User is not in room.");

        var resultString = await testUserSolutionService.CheckSolution(
            roomId.Value,
            checkSolutionRequest.Solution,
            checkSolutionRequest.IssueName);
        if (resultString.IsFailure)
            return new ApiResponse<string>(false, null, resultString.Error);

        if (CheckSolutionParser.IsResultOk(resultString.Value))
            await testUserSolutionService.UpdateUserOverhead(resultString.Value, userId, roomId.Value,
                checkSolutionRequest.LeftTime, CancellationTokenDict);

        return new ApiResponse<string>(true, resultString.Value, null);
    }

    private async Task AddUserToGroup(Guid roomId) =>
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());

    private async Task SendMessageToAllUsersInGroup<T>(Guid roomId, T message, string methodName) =>
        await Clients.Group(roomId.ToString()).SendAsync(methodName, message);
}