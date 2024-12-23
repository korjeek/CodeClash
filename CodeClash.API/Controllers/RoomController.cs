using CodeClash.API.Extensions;
using CodeClash.Core.Models.RoomsRequests;
using CodeClash.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeClash.API.Controllers;

[ApiController]
[Route("rooms")]
public class RoomController(RoomsRepository roomsRepository) : ControllerBase
{
    [HttpGet("get-rooms")]
    public async Task<IActionResult> GetRooms()
    {
        //TODO: Get list of active rooms
        throw new NotImplementedException();
    }
}