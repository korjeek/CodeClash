using System.Security.Claims;
using CodeClash.API.Extensions;
using CodeClash.API.Services;
using CodeClash.Core.Models;
using CodeClash.Core.Models.RoomsRequests;
using CodeClash.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace CodeClash.API.Hubs;

[Authorize]
[EnableCors("CorsPolicy")]
public class RoomHub(RoomService roomService, TestUserSolutionService testUserSolutionService) : Hub
{
    public async Task<string?> CreateRoom(CreateRoomRequest request)
    {
        var userId = Context.User!.GetUserIdFromAccessToken();
        var room = await roomService.CreateRoom(request.Time, request.IssueId, userId);
        // ошибочное решение await testUserSolutionService.CheckSolution("namespace CodeClash.UserSolutionTest;\npublic class SolutionTask\n{\n\tpublic int[] FindSum(int[] nums, int target)\n\t{\n\t\tvar result = new int[2];\n\t\tfor(var i = 0; i < nums.Length; i++)\n\t\t\tfor (var j = i + 1; j < nums.Length; j++)\n\t\t\t{\n\t\t\t\tif (nums[i] + nums[j] == target)\n\t\t\t\t{\n\t\t\t\t\tresult[0] = i;\n\t\t\t\t\tresult[1] = i;\n\t\t\t\t}\n\t\t\t}\n\t\treturn result;\n\t}\n}\n");
        await testUserSolutionService.CheckSolution("namespace CodeClash.UserSolutionTest;\npublic class SolutionTask\n{\n\tpublic int[] FindSum(int[] nums, int target)\n\t{\n\t\tvar result = new int[2];\n\t\tfor(var i = 0; i < nums.Length; i++)\n\t\t\tfor (var j = i + 1; j < nums.Length; j++)\n\t\t\t{\n\t\t\t\tif (nums[i] + nums[j] == target)\n\t\t\t\t{\n\t\t\t\t\tresult[0] = i;\n\t\t\t\t\tresult[1] = j;\n\t\t\t\t}\n\t\t\t}\n\t\treturn result;\n\t}\n}\n",
            "FindSum");

        // Что то вернули на какую то функцию
        //await Clients.User(Context.ConnectionId).SendAsync("createRoom", room);
        return "";
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
    
    public async Task<string> CheckSolution(string solution, string issueName)
    {
        // await testUserSolutionService.CheckSolution("namespace CodeClash.UserSolutionTest;\npublic class SolutionTask\n{\n\tpublic int[] FindSum(int[] nums, int target)\n\t{\n\t\tvar result = new int[2];\n\t\tfor(var i = 0; i < nums.Length; i++)\n\t\t\tfor (var j = i + 1; j < nums.Length; j++)\n\t\t\t{\n\t\t\t\tif (nums[i] + nums[j] == target)\n\t\t\t\t{\n\t\t\t\t\tresult[0] = i;\n\t\t\t\t\tresult[1] = j;\n\t\t\t\t}\n\t\t\t}\n\t\treturn result;\n\t}\n}\n");
        return await testUserSolutionService.CheckSolution(solution, issueName);
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