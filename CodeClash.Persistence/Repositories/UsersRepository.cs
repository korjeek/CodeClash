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

    public async Task<User?> FindUserByEmail(string email)
    {
        var user = await dbContext.Users
            .Include(u => u.Room)
            .ThenInclude(r => r!.Participants)
            .FirstOrDefaultAsync(user => user.Email == email);
        if (user is null) return null;
        if (user.Room is null) return user;
        await dbContext.Entry(user.Room).Reference(r => r.Issue).LoadAsync();
        
        return user;
    }

    public async Task UpdateUserRefreshToken(User user)
    {
        await dbContext.Users
            .Where(u => u.Id == user.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.RefreshToken, user.RefreshToken)
                .SetProperty(u => u.RefreshTokenExpiryTime, user.RefreshTokenExpiryTime));
    }

    public async Task<User?> GetUserById(Guid userId)
    {
        return await dbContext.Users
            .FindAsync(userId);
    }
}