using CodeClash.API.Services;
using CodeClash.Core.Models.RoomsRequests;
using CodeClash.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeClash.API.Controllers;

[ApiController]
[Route("room")]
public class RoomController(RoomService roomService) : ControllerBase
{
    [HttpPost("create-room")]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var res = await roomService.CreateRoom(request.Time, request.Issue);
        return Ok(res);
    }
    
    [HttpPost("enter-room")]
    public async Task<IActionResult> EnterRoomByKey([FromBody] EnterRoomRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok();
    }

    [HttpPost("start-competition")]
    public async Task<IActionResult> StartCompetition([FromBody] StartCompetitionRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok();
    }
}