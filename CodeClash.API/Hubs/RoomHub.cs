using CodeClash.API.Services;
using CodeClash.Core.Models;
using CodeClash.Core.Models.RoomsRequests;
using Microsoft.AspNetCore.SignalR;

namespace CodeClash.API.Hubs;

public class RoomHub(RoomService roomService) : Hub
{
    public async Task<(string, string)> CreateRoom(CreateRoomRequest request)
    {
        // var result = await roomService.CreateRoom(request);
        // await Groups.AddToGroupAsync(Context.ConnectionId, request.);
        //
        // return (result.Id.ToString(), result.Admin.ToString());
        throw new NotImplementedException();
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

    // public async Task<Answer> CheckSolution(string solution)
    // {
    //     
    // }
}