using CodeClash.Application.Extensions;
using CodeClash.Core.Models;
using CodeClash.Persistence.Repositories;
using CSharpFunctionalExtensions;

namespace CodeClash.Application.Services;

public class IssueService(IssuesRepository repository)
{
    public async Task<Result<Issue>> GetIssue(Guid id)
    {
        var issueEntity = await repository.GetIssueById(id);
        if (issueEntity is null)
            return Result.Failure<Issue>($"Issue with {id} does not exist.");
        return Result.Success(issueEntity.GetIssueFromEntity());
    }
}