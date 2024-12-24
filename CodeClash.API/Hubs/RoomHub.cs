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
public class RoomHub(RoomService roomService, TestUserSolutionService testUserSolutionService, CompetitionService competitionService) : Hub
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
        
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        await Clients.Group(roomId.ToString()).SendAsync("UserJoined", "HELLO MOUTHERFUCKERS!!!!");
        return new ApiResponse<RoomDTO>(true, roomResult.Value.GetRoomDTOFromRoom(), null);
    }
    
    public async Task<ApiResponse<string>> QuitRoom(Guid roomId)
    {
        var userId = Context.User.GetUserIdFromAccessToken();
        var result = await roomService.QuitRoom(userId, roomId, Context, Groups);
        if (result.IsFailure)
            return new ApiResponse<string>(false, null, result.Error);
        
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
        return new ApiResponse<string>(true, result.Value, null);
    }

    // public async Task<ApiResponse<>> StartCompetition(Guid roomId)
    // {
    //     var userId = Context.User.GetUserIdFromAccessToken();
    // }
    
    public async Task<ApiResponse<string>> CheckSolution(string solution, string issueName)
    {
        // TODO: Подумать, как это УВЛАЖНИТЬ)))) (убрать DRY)
        var roomStatus = await competitionService.GetRoomStatus(roomId);
        if (roomStatus.IsFailure)
            return new ApiResponse<string>(false, null, roomStatus.Error);
        if (roomStatus.Value is RoomStatus.CompetitionInProgress)
            return new ApiResponse<string>(false, null, "Competition in progress");
        
        var result = await competitionService.UpdateRoomStatus(roomId, RoomStatus.CompetitionInProgress);
        if (result.IsFailure)
            return new ApiResponse<string>(false, null, result.Error);
        _ = competitionService.SyncTimers(Clients.Group(roomId.ToString()), duration, roomId);

        return new ApiResponse<string>(true, "Sol GoodMan", null);
    }

    public async Task<ApiResponse<string>> CheckSolution(Guid roomId, string solution, string issueName)
    {
        // await testUserSolutionService.CheckSolution("namespace CodeClash.UserSolutionTest;\npublic class SolutionTask\n{\n\tpublic int[] FindSum(int[] nums, int target)\n\t{\n\t\tvar result = new int[2];\n\t\tfor(var i = 0; i < nums.Length; i++)\n\t\t\tfor (var j = i + 1; j < nums.Length; j++)\n\t\t\t{\n\t\t\t\tif (nums[i] + nums[j] == target)\n\t\t\t\t{\n\t\t\t\t\tresult[0] = i;\n\t\t\t\t\tresult[1] = j;\n\t\t\t\t}\n\t\t\t}\n\t\treturn result;\n\t}\n}\n");
        var roomStatus = await competitionService.GetRoomStatus(roomId);
        if (roomStatus.IsFailure)
            return new ApiResponse<string>(false, null, roomStatus.Error);
        if (roomStatus.Value == RoomStatus.CompetitionInProgress)
            return new ApiResponse<string>(false, null, "Competition in progress.");

        var resultString = await testUserSolutionService.CheckSolution(solution, issueName);
        if (resultString.IsFailure)
            return new ApiResponse<string>(false, null, resultString.Error);

        return new ApiResponse<string>(true, resultString.Value, null);
    }
}