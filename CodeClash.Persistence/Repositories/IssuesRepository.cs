using CodeClash.Core.Extensions;
using CodeClash.Core.Models;
using CSharpFunctionalExtensions;

namespace CodeClash.Persistence.Repositories;

public class IssuesRepository(ApplicationDbContext dbContext)
{
    public async Task Add(Issue issue)
    {
        var issueEntity = issue.GetIssueEntity();
        await dbContext.Issues.AddAsync(issueEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Issue?> GetIssueById(Guid issueId)
    {
        var issueEntity =  await dbContext.Issues
            .FindAsync(issueId);
        return issueEntity?.GetIssueFromEntity();
    }
}