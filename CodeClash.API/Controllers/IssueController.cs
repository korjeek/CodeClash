using CodeClash.Application.Extensions;
using CodeClash.Core.Models;
using CodeClash.Core.Models.DTOs;
using CodeClash.Core.Requests.IssueRequest;
using CodeClash.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeClash.API.Controllers;

[Route("issue")]
public class IssueController(IssuesRepository issuesRepository) : ControllerBase
{
    [HttpPost("add-issue")]
    public async Task<IActionResult> AddIssue([FromBody] CreateIssueRequest request)
    {
        // TODO: добавить проверку на пароль админа по задачам, может придумать другой способ добавлять задачи
        var newIssueResult = Issue.Create(Guid.NewGuid(), request.Description, request.Name);
        if (newIssueResult.IsFailure)
            return BadRequest(newIssueResult.Error);
        await issuesRepository.Add(newIssueResult.Value.GetIssueEntity());
        return Ok(newIssueResult.Value.GetIssueDTO());
    }

    [HttpGet("get-issues")]
    public async Task<IActionResult> GetIssues()
    {
        var issues = await issuesRepository.GetAllIssues();
        return Ok(issues.Select(i => new IssueDTO {Id = i.Id.ToString(), Name = i.Name}).ToList());
    }
}