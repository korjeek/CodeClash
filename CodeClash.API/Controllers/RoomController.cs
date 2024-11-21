using CodeClash.API.Services;
using CodeClash.Core.Models.RoomsRequests;
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
        
        var result = await roomService.CreateRoom(request);
        if (result == null)
            return BadRequest("Wrong issue id");
        
        return Ok(result);
    }
    
    [HttpPost("enter-room")]
    public async Task<IActionResult> EnterRoomByKey([FromBody] EnterRoomRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await roomService.EnterRoom(request);
        if (result == null)
            return BadRequest("The room does not exist or competition in progress");

        return Ok(result);
    }
    
    [HttpPost("start-competition")]
    public async Task<IActionResult> StartCompetition([FromBody] StartCompetitionRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        
        
        return Ok();
    }
    
    // [HttpPost("finish-competition")]
    // public async Task<IActionResult> FinishCompetition([FromBody] FinishCompetitionRequest request)
    // {
    //     if (!ModelState.IsValid)
    //         return BadRequest(ModelState);
    //     
    //     return Ok();
    // }
}