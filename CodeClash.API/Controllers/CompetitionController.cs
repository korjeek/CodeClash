using CodeClash.API.Services;
using CodeClash.Core.Models.CompetitionRequests;
using Microsoft.AspNetCore.Mvc;

namespace CodeClash.API.Controllers;


[ApiController]
[Route("competition")]
public class CompetitionController(CompetitionService service) : ControllerBase
{
    [HttpPost("send-solution")]
    public async Task<IActionResult> CheckSolution([FromBody] SolutionRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        return Ok();
    }
}