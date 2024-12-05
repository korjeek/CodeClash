using CodeClash.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence.Repositories;

public class UsersRepository(ApplicationDbContext dbContext)
{
    public async Task<User?> AddUser(User user)
    {
        var userEntity = user.GetEntity();
        var isUserEmailContainsInDb = await dbContext.Users
            .Select(u => u.Email)
            .ContainsAsync(userEntity.Email);
        if (isUserEmailContainsInDb)
            return null;
        await dbContext.AddAsync(userEntity);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<UserEntity?> FindUserByEmail(string email)
    {
        var user = await dbContext.Users
            .Include(u => u.Room)
            .ThenInclude(r => r!.Participants)
            .FirstOrDefaultAsync(user => user.Email == email);
        if (user is null) return null;
        if (user.Room is null) return user;
        await dbContext.Entry(user.Room).Reference(r => r.IssueEntity).LoadAsync();
        
        return user;
    }

    public async Task UpdateUserRefreshToken(UserEntity userEntity)
    {
        await dbContext.Users
            .Where(u => u.Id == userEntity.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.RefreshToken, userEntity.RefreshToken)
                .SetProperty(u => u.RefreshTokenExpiryTime, userEntity.RefreshTokenExpiryTime));
    }

    public async Task<UserEntity?> GetUserById(Guid userId)
    {
        return await dbContext.Users
            .FindAsync(userId);
    }
}