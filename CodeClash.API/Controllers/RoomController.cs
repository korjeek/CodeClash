using CodeClash.API.Extensions;
using CodeClash.API.Services;
using CodeClash.Core.Models.RoomsRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeClash.API.Controllers;

[ApiController]
[Authorize]
[Route("room")]
public class RoomController(RoomService roomService) : ControllerBase
{
    // TODO: Websocket connection
    [HttpPost("create-room")]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequest request)
    {
        var userId = User.GetUserIdFromAccessToken();
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result = await roomService.CreateRoom(request.Time, request.IssueId, userId);
        if (result == null)
            return BadRequest("Wrong issue id");
        
        return Ok($"Room is created id: {result.Id}");
    }
    
    // TODO: Websocket connection
    [HttpPost("enter-room")]
    public async Task<IActionResult> EnterRoomByKey([FromBody] EnterQuitRoomRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var userId = User.GetUserIdFromAccessToken();
        var result = await roomService.EnterRoom(request.RoomId, userId);
        if (result == null)
            return BadRequest("The room does not exist or competition in progress");

        return Ok("User is added to Room");
    }
    
    [HttpPost("quit-room")]
    public async Task<IActionResult> QuitRoom([FromBody] EnterQuitRoomRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.GetUserIdFromAccessToken();
        var result = await roomService.QuitRoom(request.RoomId, userId);
        if (result == null)
            return BadRequest("The room does not exist");

        return Ok("You quited from room");
    }
    
    [HttpPost("close-room")]
    public async Task<IActionResult> CloseRoom([FromBody] Guid roomId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("start-competition")]
    public async Task<IActionResult> StartCompetition([FromBody] Guid roomId)
    {
        throw new NotImplementedException();
    }
}