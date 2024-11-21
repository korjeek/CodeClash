using CodeClash.Core.Models;
using CodeClash.Persistence.Repositories;

namespace CodeClash.API.Services;

public class IssueService(IssuesRepository repository)
{
    public async Task<Issue?> GetIssue(Guid id)
    {
        return await repository.GetIssueById(id);
    }
}