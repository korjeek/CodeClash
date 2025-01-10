using CodeClash.Application.Extensions;
using CodeClash.Application.Services;
using CodeClash.Core.Models.DTOs;
using CodeClash.Persistence.Entities;
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
        var roomsResult = await roomService.GetAllWaitingRoomDtos();
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
        var userId = Request.GetUserIdFromCookie();
        var isUserAdminResult = await roomService.IsUserAdmin(userId);
        if (isUserAdminResult.IsFailure)
            return new ApiResponse<bool?>(false, null, isUserAdminResult.Error);
        return new ApiResponse<bool?>(true, isUserAdminResult.Value, null);
    }

    [HttpGet("get-room-info")]
    public async Task<ApiResponse<RoomDTO>> GetRoomInfo()
    {
        var userId = Request.GetUserIdFromCookie();
        var roomIdResult = await userService.GetUserRoomId(userId);
        if (roomIdResult.IsFailure)
            return new ApiResponse<RoomDTO>(false, null, roomIdResult.Error);
        var roomResult = await roomService.GetRoomByRoomId(roomIdResult.Value);
        if (roomResult.IsFailure)
            return new ApiResponse<RoomDTO>(false, null, roomResult.Error);
        return new ApiResponse<RoomDTO>(true, roomResult.Value.GetRoomDtoFromRoom(), null);
    }

    [HttpGet("get-room-status")]
    public async Task<ApiResponse<RoomStatus?>> GetRoomStatus()
    {
        var userId = Request.GetUserIdFromCookie();
        var roomIdResult = await userService.GetUserRoomId(userId);
        if (roomIdResult.IsFailure)
            return new ApiResponse<RoomStatus?>(false, null, roomIdResult.Error);
        var roomEntityResult = await roomService.GetRoomEntityById(roomIdResult.Value);
        if (roomEntityResult.IsFailure)
            return new ApiResponse<RoomStatus?>(false, null, roomEntityResult.Error);
        return new ApiResponse<RoomStatus?>(true, roomEntityResult.Value.Status, null);
    }
}