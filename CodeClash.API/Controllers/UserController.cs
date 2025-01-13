using CodeClash.Application.Services;
using CodeClash.Core.Models.DTOs;
using CodeClash.Persistence.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeClash.API.Controllers;

[ApiController]
[Authorize]
[Route("user")]
public class UserController(UserService userService, RoomService roomService) : ControllerBase
{
    [HttpGet("get-user-state")]
    public async Task<ApiResponse<UserStateDTO>> GetUserState()
    {
        var userId = Request.GetUserIdFromAuthorizedUserCookie();
        var roomIdResult = await userService.GetUserRoomId(userId);
        if (roomIdResult.IsFailure)
            return new ApiResponse<UserStateDTO>(true,
                new UserStateDTO
                {
                    HasRoom = false,
                    CompetitionIssueId = null
                },
                null);
        var roomEntityResult = await roomService.GetRoomEntityById(roomIdResult.Value);
        if (roomEntityResult.IsFailure)
            return new ApiResponse<UserStateDTO>(false, null, roomEntityResult.Error);

        var competitionIssueId = roomEntityResult.Value.Status is RoomStatus.CompetitionInProgress
            ? roomEntityResult.Value.IssueId.ToString()
            : null;
        return new ApiResponse<UserStateDTO>(true, new UserStateDTO
            {
                HasRoom = true,
                CompetitionIssueId = competitionIssueId
            },
            null);
    }
}