using CodeClash.API.Extensions;
using CodeClash.Application.Extensions;
using CodeClash.Application.Services;
using CodeClash.Core.Models.DTOs;
using CodeClash.Core.Models.RoomsRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace CodeClash.API.Hubs;

[Authorize]
[EnableCors("CorsPolicy")]
public class RoomHub(RoomService roomService, TestUserSolutionService testUserSolutionService) : Hub
{
    public async Task<ApiResponse<RoomDTO>> CreateRoom(CreateRoomRequest request)
    {
        var userId = Context.User.GetUserIdFromAccessToken();
        var roomResult =  await roomService.CreateRoom(request.RoomName, request.Time, request.IssueId, userId);
        if (roomResult.IsFailure)
            return new ApiResponse<RoomDTO>(false, null, roomResult.Error);

        var room = roomResult.Value;

        // Что то вернули на какую то функцию
        //await Clients.User(Context.ConnectionId).SendAsync("createRoom", room);

        await Groups.AddToGroupAsync(Context.ConnectionId, room.Id.ToString());
        return new ApiResponse<RoomDTO>(true, roomResult.Value.GetRoomDTOFromRoom(), null);
    }
    
    public async Task<ApiResponse<RoomDTO>> JoinRoom(Guid roomId)
    {
        var userId = Context.User.GetUserIdFromAccessToken();
        var roomResult = await roomService.JoinRoom(roomId, userId);
        if (roomResult.IsFailure)
            return new ApiResponse<RoomDTO>(false, null, roomResult.Error);

        // TODO: сообщить другим пользователям, нужна специальная функция на фронте:
        // await 
        // await Clients.Group(roomId.ToString()).SendCoreAsync()
        
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        await Clients.Group(roomId.ToString()).SendAsync("UserJoined", "HELLO MOUTHERFUCKERS!!!!");
        return new ApiResponse<RoomDTO>(true, roomResult.Value.GetRoomDTOFromRoom(), null);
    }
    
    public async Task<ApiResponse<string>> QuitRoom(Guid roomId)
    {
        var userId = Context.User.GetUserIdFromAccessToken();
        var result = await roomService.QuitRoom(userId);
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
        // var userId = Context.User.GetUserIdFromAccessToken();
        
        
        
        // await testUserSolutionService.CheckSolution("namespace CodeClash.UserSolutionTest;\npublic class SolutionTask\n{\n\tpublic int[] FindSum(int[] nums, int target)\n\t{\n\t\tvar result = new int[2];\n\t\tfor(var i = 0; i < nums.Length; i++)\n\t\t\tfor (var j = i + 1; j < nums.Length; j++)\n\t\t\t{\n\t\t\t\tif (nums[i] + nums[j] == target)\n\t\t\t\t{\n\t\t\t\t\tresult[0] = i;\n\t\t\t\t\tresult[1] = j;\n\t\t\t\t}\n\t\t\t}\n\t\treturn result;\n\t}\n}\n");


        var resultString = await testUserSolutionService.CheckSolution(solution, issueName);
        if (resultString.IsFailure)
            return new ApiResponse<string>(false, null, resultString.Error);

        return new ApiResponse<string>(true, resultString.Value, null);
        // Нужно:
        //      - чтобы по истечении таймера нельзя было попасть на страницу соревнования
        //      - чтобы по истечении таймера всех выкинуло в комнату
        // Логика работы соревнования:
        //      - таймер запускается при вызове метода StartCompetition
        //      
        //          - сразу же открывается возможность отправить задачу
        //          - при отправке задачи пользователь видит ответ системы, может отправить ещё раз
        //      - соревнование заканчивается только по истечении таймера(или если все пользователи решили задачу)
        //          - в конце соревнования всех выбрасывает в комнату, результаты можно увидеть в предыдущих резульатах
        //          - наверное, для выброса нужен метод EndCompetition
        //              - в этом методе будут подтягиваться результаты каждого пользователя
        // TODO: очередь отправленных решений...
        // через что можно сделать?
    }
}