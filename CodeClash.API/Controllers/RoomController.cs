using CodeClash.API.Services;
using CodeClash.Core.Models;
using CodeClash.Core.Models.RoomsRequests;
using Microsoft.AspNetCore.Mvc;

namespace CodeClash.API.Controllers;

[ApiController]
[Route("room")]
public class RoomController(RoomService roomService, IssueService issueService) : ControllerBase
{
    [HttpPost("create-room")]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var issue = await issueService.GetIssueById(request.IssueId);
        var res = await roomService.CreateRoom(request.Time, issue);
        return Ok(request.Time);
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