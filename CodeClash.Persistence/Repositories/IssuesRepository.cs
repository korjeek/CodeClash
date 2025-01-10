using CodeClash.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence.Repositories;

public class IssuesRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
{
    public async Task Add(IssueEntity issue)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        await dbContext.Issues.AddAsync(issue);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IssueEntity?> GetIssueById(Guid issueId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var issueEntity =  await dbContext.Issues
            .FindAsync(issueId);
        return issueEntity;
    }

    public async Task<List<IssueEntity>> GetAllIssues()
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        return await dbContext.Issues.ToListAsync();
    }

    
}