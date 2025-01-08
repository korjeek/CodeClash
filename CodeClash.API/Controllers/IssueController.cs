using CodeClash.Application.Extensions;
using CodeClash.Application.Services;
using CodeClash.Core.Models;
using CodeClash.Core.Models.DTOs;
using CodeClash.Core.Requests.IssuesRequests;
using Microsoft.AspNetCore.Mvc;

namespace CodeClash.API.Controllers;

[Route("issue")]
public class IssueController(IssueService issueService, 
    TestUserSolutionService testUserSolutionService) : ControllerBase
{
    [HttpPost("add-issue")]
    public async Task<IActionResult> AddIssue([FromBody] CreateIssueRequest request)
    {
        // TODO: добавить проверку на пароль админа по задачам, может придумать другой способ добавлять задачи
        var newIssueResult = Issue.Create(Guid.NewGuid(), request.Description, request.Name);
        if (newIssueResult.IsFailure)
            return BadRequest(newIssueResult.Error);
        await issueService.AddIssueToDb(newIssueResult.Value);
        return Ok(newIssueResult.Value.GetIssueDTO());
    }

    [HttpGet("get-all-issues")]
    public async Task<IActionResult> GetIssues()
    {
        var issues = await issueService.GetAllIssues();
        return Ok(issues.Select(i => new IssueDTO {Id = i.Id.ToString(), Name = i.Name}).ToList());
    }

    [HttpPost("get-issue")]
    public async Task<ApiResponse<IssueDTO>> GetIssue([FromBody] IssueDTO issueDto)
    {
        var issueResult = await issueService.GetIssueFromDb(new Guid(issueDto.Id));
        if (issueResult.IsFailure)
            return new ApiResponse<IssueDTO>(false, null, issueResult.Error);

        var issueDtoResponse = issueResult.Value.GetIssueDTO();
        issueDtoResponse.InitialCode = await System.IO.File.ReadAllTextAsync(testUserSolutionService.startCodeLocations[issueResult.Value.Name]);
        return new ApiResponse<IssueDTO>(true, issueDtoResponse , null);
    }
}