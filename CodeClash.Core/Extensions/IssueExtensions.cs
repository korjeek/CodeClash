using CodeClash.Core.Models;
using CodeClash.Core.Models.DTOs;
using CSharpFunctionalExtensions;

namespace CodeClash.Core.Extensions;

public static class IssueExtensions
{
    public static IssueDTO GetIssueDTO(this Issue issue) => new IssueDTO
    {
        Id = issue.Id.ToString(),
        Description = issue.Description,
        Name = issue.Name
    };

    public static IssueEntity GetIssueEntity(this Issue issue) => new IssueEntity
    {
        Id = issue.Id,
        Description = issue.Description,
        Name = issue.Name
    };

    public static Issue GetIssueFromEntity(this IssueEntity issueEntity) => Issue.Create(
        issueEntity.Id,
        issueEntity.Description,
        issueEntity.Name).Value;
}