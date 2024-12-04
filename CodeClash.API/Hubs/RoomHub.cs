using System.Security.Claims;
using CodeClash.API.Extensions;
using CodeClash.API.Services;
using CodeClash.Core.Models;
using CodeClash.Core.Models.RoomsRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CodeClash.API.Hubs;

// [Authorize]
public class RoomHub(RoomService roomService) : Hub
{
    public async Task Send(string message)
    {
        var userId = Context.User?.GetUserIdFromAccessToken();
        Console.WriteLine($"Connected user token:\n{userId}");
        await Clients.All.SendAsync("Receive", message);
    }
    
    
    
    public async Task<(string, string)> CreateRoom(CreateRoomRequest request)
    {
        
        // var userId = Context.User?.GetUserIdFromAccessToken();
        // Console.WriteLine($"Connected user token:\n{userId}");
        var httpContext = Context.GetHttpContext();
        var cookie = httpContext!.Request.Cookies["spooky-cookies"];
        Console.WriteLine(cookie);
        return ("ALL", "IS ALLRIGHT");
    }
    
    public async Task JoinRoom(Guid roomId)
    {
        var userId = Context.User!.GetUserIdFromAccessToken();
        await roomService.JoinRoom(roomId, userId);
    }

    public async Task QuitRoom(string userEmail, Guid roomId)
    {
        throw new NotImplementedException();
    }

    // public async Task<Answer> CheckSolution(string solution)
    // {
    //     
    // }
}