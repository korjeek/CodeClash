using System.Security.Claims;
using CodeClash.API.Extensions;
using CodeClash.API.Services;
using CodeClash.Core;
using CodeClash.Core.Extensions;
using CodeClash.Core.Models;
using CodeClash.Core.Models.DTOs;
using CodeClash.Core.Models.RoomsRequests;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace CodeClash.API.Hubs;

[Authorize]
[EnableCors("CorsPolicy")]
public class RoomHub(RoomService roomService) : Hub
{
    public async Task<Result<RoomDTO>> CreateRoom(CreateRoomRequest request)
    {
        var userId = Context.User.GetUserIdFromAccessToken();
        var roomResult =  await roomService.CreateRoom(request.RoomName, request.Time, request.IssueId, userId);
        if (roomResult.IsFailure)
            return Result.Failure<RoomDTO>(roomResult.Error);

        var room = roomResult.Value;
        await Groups.AddToGroupAsync(Context.ConnectionId, room.Id.ToString());


        // Что то вернули на какую то функцию
        //await Clients.User(Context.ConnectionId).SendAsync("createRoom", room);
        return Result.Success(room.GetRoomDTO());
    }
    
    public async Task<Result<RoomDTO>> JoinRoom(Guid roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        
        var userId = Context.User.GetUserIdFromAccessToken();
        
        var roomResult = await roomService.JoinRoom(roomId, userId);
        if (roomResult.IsFailure)
            return Result.Failure<RoomDTO>(roomResult.Error);
        return roomResult.Value.GetRoomDTO();
    }

    public async Task<RoomEntity?> QuitRoom(Guid roomId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
        
        // var userId = Context.User
        // return await roomService.QuitRoom(roomId,);
        throw new NotImplementedException();
    }

    // public async Task<Answer> CheckSolution(string solution)
    // {
    //     
    // }
}