using System.Security.Claims;
using CodeClash.API.Extensions;
using CodeClash.API.Services;
using CodeClash.Core;
using CodeClash.Core.Models;
using CodeClash.Core.Models.RoomsRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace CodeClash.API.Hubs;

[Authorize]
[EnableCors("CorsPolicy")]
public class RoomHub(RoomService roomService) : Hub
{
    public async Task<Room?> CreateRoom(CreateRoomRequest request)
    {
        var userId = Context.User!.GetUserIdFromAccessToken();
        var room =  await roomService.CreateRoom(request.Time, request.IssueId, userId);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, room.Id.ToString());


        // Что то вернули на какую то функцию
        //await Clients.User(Context.ConnectionId).SendAsync("createRoom", room);
        return room;
    }
    
    public async Task<RoomEntity?> JoinRoom(Guid roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        
        var userId = Context.User!.GetUserIdFromAccessToken();
        return await roomService.JoinRoom(roomId, userId);
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