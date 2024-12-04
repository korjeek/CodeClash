using System.Security.Claims;
using CodeClash.API.Extensions;
using CodeClash.API.Services;
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

        // Что то вернули на какую то функцию
        //await Clients.User(Context.ConnectionId).SendAsync("createRoom", room);
        return room;
    }
    
    public async Task<Room?> JoinRoom(Guid roomId)
    {
        var userId = Context.User!.GetUserIdFromAccessToken();
        return await roomService.JoinRoom(roomId, userId);
    }

    public async Task QuitRoom(Guid roomId)
    {
        throw new NotImplementedException();
    }

    // public async Task<Answer> CheckSolution(string solution)
    // {
    //     
    // }
}