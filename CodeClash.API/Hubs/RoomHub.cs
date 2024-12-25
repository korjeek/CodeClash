using CodeClash.API.Extensions;
using CodeClash.Application.Extensions;
using CodeClash.Application.Services;
using CodeClash.Core.Models.DTOs;
using CodeClash.Core.Models.RoomsRequests;
using CodeClash.Persistence.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace CodeClash.API.Hubs;

[Authorize]
[EnableCors("CorsPolicy")]
public class RoomHub(RoomService roomService, 
    TestUserSolutionService testUserSolutionService, 
    CompetitionService competitionService, 
    IssueService issueService) : Hub
{
    public async Task<ApiResponse<RoomDTO>> CreateRoom(CreateRoomRequest request)
    {
        var userId = Context.User.GetUserIdFromAccessToken();
        var roomResult =  await roomService.CreateRoom(request.RoomName, request.Time, request.IssueId, userId);
        if (roomResult.IsFailure)
            return new ApiResponse<RoomDTO>(false, null, roomResult.Error);
        
        var room = roomResult.Value;
        await Groups.AddToGroupAsync(Context.ConnectionId, room.Id.ToString());
        return new ApiResponse<RoomDTO>(true, room.GetRoomDTOFromRoom(), null);
    }
    
    public async Task<ApiResponse<RoomDTO>> JoinRoom(Guid roomId)
    {
        var userId = Context.User.GetUserIdFromAccessToken();
        var roomResult = await roomService.JoinRoom(roomId, userId);
        if (roomResult.IsFailure)
            return new ApiResponse<RoomDTO>(false, null, roomResult.Error);
        
        await Clients.Group(roomId.ToString()).SendAsync("UserJoined", roomResult.Value.GetRoomDTOFromRoom());
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        return new ApiResponse<RoomDTO>(true, roomResult.Value.GetRoomDTOFromRoom(), null);
    }
    
    public async Task<ApiResponse<string>> QuitRoom(Guid roomId)
    {
        var userId = Context.User.GetUserIdFromAccessToken();
        var result = await roomService.QuitRoom(userId, roomId, Context, Groups);
        if (result.IsFailure)
            return new ApiResponse<string>(false, null, result.Error);
        return new ApiResponse<string>(true, result.Value, null);
    }
    
    public async Task<ApiResponse<string>> StartCompetition(Guid roomId, TimeOnly duration)
    {
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

        await Clients.Group(roomId.ToString()).SendAsync("CompetitionStarted", $"/problem/{roomEntityResult.Value.IssueId}");
        _ = competitionService.SyncTimers(Clients.Group(roomId.ToString()), duration, roomId);
        
        return new ApiResponse<string>(true, "Competition is started", null);
    }

    public async Task<ApiResponse<IssueDTO>> GetIssue(Guid issueId)
    {
        var issueResult = await issueService.GetIssue(issueId);
        if (issueResult.IsFailure)
            return new ApiResponse<IssueDTO>(false, null, issueResult.Error);

        var issueDTO = issueResult.Value.GetIssueDTO();
        issueDTO.InitialCode = await File.ReadAllTextAsync(testUserSolutionService.startCodeLocations[issueResult.Value.Name]);
        return new ApiResponse<IssueDTO>(true, issueDTO , null);
    }
    
    public async Task<ApiResponse<string>> CheckSolution(Guid roomId, string solution, string issueName)
    {
        var resultString = await testUserSolutionService.CheckSolution(roomId, solution, issueName);
        if (resultString.IsFailure)
            return new ApiResponse<string>(false, null, resultString.Error);
        
        return new ApiResponse<string>(true, resultString.Value, null);
    }

    public async Task<ApiResponse<bool?>> CheckIsUserAdmin()
    {
        var userId = Context.User.GetUserIdFromAccessToken();
        var isAdminResult = await roomService.IsUserAdmin(userId);
        if (isAdminResult.IsFailure)
            return new ApiResponse<bool?>(false, null, isAdminResult.Error);
        return new ApiResponse<bool?>(true, isAdminResult.Value, null);
    }
}