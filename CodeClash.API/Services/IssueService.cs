using CodeClash.Core.Models;
using CodeClash.Persistence.Repositories;
using CSharpFunctionalExtensions;

namespace CodeClash.API.Services;

public class IssueService(IssuesRepository repository)
{
    public async Task<Result<Issue>> GetIssue(Guid id)
    {
        return await repository.GetIssueById(id);
    }
}