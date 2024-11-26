using CodeClash.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence.Repositories;

public class IssuesRepository(ApplicationDbContext dbContext)
{
    public async Task<Issue?> Add(Issue issue)
    {
        await dbContext.AddAsync(issue);
        await dbContext.SaveChangesAsync();
        
        return issue;
    }

    public async Task<Issue?> GetIssueById(Guid id)
    {
        return await dbContext.Issues
            .AsNoTracking()
            .FirstOrDefaultAsync(issue => issue.Id == id);
    }
}