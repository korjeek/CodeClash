using CodeClash.Core.Models;

namespace CodeClash.Persistence.Repositories;

public class IssuesRepository(ApplicationDbContext dbContext)
{
    public async Task<Issue?> Add(Issue issue)
    {
        await dbContext.Issues.AddAsync(issue);
        await dbContext.SaveChangesAsync();
        
        return issue;
    }

    public async Task<Issue?> GetIssueById(Guid issueId)
    {
        return await dbContext.Issues
            .FindAsync(issueId);
    }
}