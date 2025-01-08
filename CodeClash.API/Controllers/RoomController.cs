using System.Security.Claims;
using CodeClash.Application.Extensions;
using CodeClash.Application.Services;
using CodeClash.Core.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeClash.API.Controllers;

[ApiController]
[Authorize]
[Route("rooms")]
public class RoomController(RoomService roomService, UserService userService) : ControllerBase
{
    [HttpGet("get-rooms")]
    public async Task<ApiResponse<List<RoomDTO>>> GetRooms()
    {
        var roomsResult = await roomService.GetAllWaitingRoomDTOs();
        if (roomsResult.IsFailure)
            return new ApiResponse<List<RoomDTO>>(false, null, roomsResult.Error);
        return new ApiResponse<List<RoomDTO>>(
            true,
            roomsResult.Value,
            null
        );
    }
    
    [HttpGet("check-for-admin")]
    public async Task<ApiResponse<bool?>> CheckUserIsAdmin()
    {
        Request.Cookies.TryGetValue("spooky-cookies", out string? cookie);
        var userId = new Guid(cookie!.GetClaimsFromToken().First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        var isUserAdminResult = await roomService.IsUserAdmin(userId);
        if (isUserAdminResult.IsFailure)
            return new ApiResponse<bool?>(false, null, isUserAdminResult.Error);
        return new ApiResponse<bool?>(true, isUserAdminResult.Value, null);
    }

    [HttpGet("get-room-info")]
    public async Task<ApiResponse<RoomDTO>> GetRoomInfo()
    {
        Request.Cookies.TryGetValue("spooky-cookies", out string? cookie);
        var userId = new Guid(cookie!.GetClaimsFromToken().First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        var roomId = await userService.GetUserRoomId(userId);
        if (!roomId.HasValue)
            return new ApiResponse<RoomDTO>(false, null, "User has not room.");
        var roomResult = await roomService.GetRoom(roomId.Value);
        if (roomResult.IsFailure)
            return new ApiResponse<RoomDTO>(false, null, roomResult.Error);
        return new ApiResponse<RoomDTO>(true, roomResult.Value.GetRoomDTOFromRoom(), null);
    }

    [HttpPost("get-room-leaders")]
    public async Task<ApiResponse<List<UserDTO>>> GetRoomLeaders(Guid roomId)
    {
        throw new NotImplementedException();
    }
}