using System.Security.Claims;
using CodeClash.Application.Extensions;
using CodeClash.Application.Services;
using CodeClash.Core;
using CodeClash.Core.Requests.SolutionsRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeClash.API.Controllers;

[ApiController]
[Authorize]
[Route("competition")]
public class CompetitionController(TestUserSolutionService testUserSolutionService, UserService userService) : ControllerBase
{
    [HttpPost("check-solution")]
    public async Task<ApiResponse<string>> CheckSolution([FromBody] CheckSolutionRequest checkSolutionRequest)
    {
        Request.Cookies.TryGetValue("spooky-cookies", out string? cookie);
        var userId = new Guid(cookie!.GetClaimsFromToken().First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        var roomId = await userService.GetUserRoomId(userId);
        if (!roomId.HasValue)
            return new ApiResponse<string>(false, null, "User is not in room.");
        
        var resultString = await testUserSolutionService.CheckSolution(
            roomId.Value, 
            checkSolutionRequest.Solution, 
            checkSolutionRequest.IssueName);
        if (resultString.IsFailure)
            return new ApiResponse<string>(false, null, resultString.Error);
        
        if (CheckSolutionParser.IsResultOk(resultString.Value))
            await testUserSolutionService.UpdateUserOverhead(resultString.Value, userId, checkSolutionRequest.LeftTime);
        
        return new ApiResponse<string>(true, resultString.Value, null);
    }
}