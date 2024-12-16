using CodeClash.Core.Models;
using CodeClash.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeClash.API.Controllers;

public class IssueController(IssuesRepository issuesRepository) : ControllerBase
{
    [HttpPost("add-issue")]
    public async Task<IActionResult> AddIssue([FromBody] string description, [FromBody] string issueAdminPasswd)
    {
        throw new NotImplementedException();
    }
}
