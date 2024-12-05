using CodeClash.API.Services;
using CodeClash.Core.Models.RoomsRequests;
using CodeClash.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CodeClash.API.Hubs;

[Authorize]
public class RoomHub(RoomService roomService, TestUserSolutionService testUserSolutionService) : Hub
{
    public async Task<(string, string)> CreateRoom(CreateRoomRequest request)
    {
        // var result = await roomService.CreateRoom(request);
        // await Groups.AddToGroupAsync(Context.ConnectionId, request.);
        //
        // return (result.Id.ToString(), result.Admin.ToString());
        Console.WriteLine("HUI");
        throw new NotImplementedException();
    }
    
    public async Task Send(string message)
    {
        await Clients.All.SendAsync("Receive", message);
    }
    
    public async Task JoinRoom(string userEmail, Guid roomId)
    {
        // логика взаимодействия с моделями есть, осталось отправить ответ
        throw new NotImplementedException();
    }

    public async Task QuitRoom(string userEmail, Guid roomId)
    {
        throw new NotImplementedException();
    }

    public async Task CheckSolution(string solution)
    {
        await testUserSolutionService.CheckSolution(solution);
    }
}