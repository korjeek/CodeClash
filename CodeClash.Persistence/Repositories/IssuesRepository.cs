using CodeClash.Core.Models;

namespace CodeClash.Persistence.Repositories;

public class IssuesRepository(ApplicationDbContext dbContext)
{
    public async Task<IssueEntity?> Add(IssueEntity issueEntity)
    {
        await dbContext.Issues.AddAsync(issueEntity);
        await dbContext.SaveChangesAsync();
        
        return issueEntity;
    }

    public async Task<IssueEntity?> GetIssueById(Guid issueId)
    {
        return await dbContext.Issues
            .FindAsync(issueId);
    }
}