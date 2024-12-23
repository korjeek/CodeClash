using CodeClash.Application.Extensions;
using CodeClash.Application.Services;
using CodeClash.Core.Models.DTOs;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace CodeClash.API.Controllers;

[ApiController]
[Route("rooms")]
public class RoomController(RoomService roomService) : ControllerBase
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
}