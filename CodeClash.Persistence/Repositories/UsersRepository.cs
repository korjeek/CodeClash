using CodeClash.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence.Repositories;

public class UsersRepository(ApplicationDbContext dbContext)
{
    public async Task Add(User user)
    {
        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<User?> FindUserByEmail(string email)
    {
        return await dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == email);
    }
}