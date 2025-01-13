using CodeClash.Application.Extensions;
using CodeClash.Application.Services;
using CodeClash.Core.Models;
using CodeClash.Core.Models.DTOs;
using CodeClash.Core.Requests.IssuesRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeClash.API.Controllers;

[ApiController]
[Authorize]
[Route("issue")]
public class IssueController(IssueService issueService, 
    TestUserSolutionService testUserSolutionService,
    UserService userService, IConfiguration configuration) : ControllerBase
{
    [HttpPost("add-issue")]
    public async Task<ApiResponse<IssueDTO>> AddIssue([FromBody] CreateIssueRequest request)
    {
        var userId = Request.GetUserIdFromAuthorizedUserCookie();
        var userEntity = await userService.GetUser(userId);
        if (userEntity.IsFailure)
            return new ApiResponse<IssueDTO>(false, null, userEntity.Error);
        if (userEntity.Value.Email != configuration["AdminUser:AdminEmail"]! ||
            userEntity.Value.PasswordHash != configuration["AdminUser:AdminPasswordHash"])
            return new ApiResponse<IssueDTO>(false, null, "Only administrator can add issues!");
        
        var newIssueResult = Issue.Create(Guid.NewGuid(), request.Description, request.Name);
        if (newIssueResult.IsFailure)
            return new ApiResponse<IssueDTO>(false, null, newIssueResult.Error);
        await issueService.AddIssueToDb(newIssueResult.Value);
        return new ApiResponse<IssueDTO>(true, newIssueResult.Value.GetIssueDTO(), null);
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