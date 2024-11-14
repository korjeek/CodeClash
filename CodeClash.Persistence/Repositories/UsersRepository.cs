using CodeClash.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence.Repositories;

public class UsersRepository(ApplicationDbContext dbContext)
{
    public async Task<User?> AddUser(User user)
    {
        var isUserEmailContainsInDb = await dbContext.Users
            .Select(u => u.Email)
            .ContainsAsync(user.Email);
        if (isUserEmailContainsInDb)
            return null;
        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> FindUserByUserName(string? userName)
    {
        return await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.UserName == userName);
    }

    public async Task<User?> FindUserByEmail(string email)
    {
        return await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == email);
    }

    public async void UpdateUsersRefreshToken(Guid id, string newRefreshToken)
    {
        // user.RefreshToken = newRefreshToken;
        await dbContext.Users
            .Where(user => user.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.RefreshToken, newRefreshToken));
    }

    public async Task<string> GetPassword(Guid id)
    {
        var userResult = await dbContext.Users
            .AsNoTracking()
            .FirstAsync(user => user.Id == id);
        return userResult.PasswordHash;
    }
}