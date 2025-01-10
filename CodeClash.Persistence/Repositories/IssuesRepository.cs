using CodeClash.Persistence.Entities;

namespace CodeClash.Persistence.Repositories;

public class IssuesRepository(ApplicationDbContext dbContext)
{
    public async Task Add(IssueEntity issue)
    {
        await dbContext.Issues.AddAsync(issue);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IssueEntity?> GetIssueById(Guid issueId)
    {
        var issueEntity =  await dbContext.Issues
            .FindAsync(issueId);
        return issueEntity;
    }

    public async Task<List<IssueEntity>> GetAllIssues() => dbContext.Issues.ToList();
    
}