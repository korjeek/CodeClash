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
    public async Task<ApiResponse<UserStateDTO>> GetUserState()
    {
        var userId = Request.GetUserIdFromCookie();
        var roomId = await userService.GetUserRoomId(userId);
        if (roomId is null)
            return new ApiResponse<UserStateDTO>(true,
                new UserStateDTO
                {
                    IsHasRoom = false,
                    CompetitionIssueId = null
                },
                null);
        var roomEntityResult = await roomService.GetRoomEntityById(roomId.Value);
        if (roomEntityResult.IsFailure)
            return new ApiResponse<UserStateDTO>(false, null, roomEntityResult.Error);

        var competitionIssueId = roomEntityResult.Value.Status is RoomStatus.CompetitionInProgress
            ? roomEntityResult.Value.IssueId.ToString()
            : null;
        return new ApiResponse<UserStateDTO>(true, new UserStateDTO
            {
                IsHasRoom = true,
                CompetitionIssueId = competitionIssueId
            },
            null);
    }
}