using System.Linq.Expressions;
using CodeClash.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence.Repositories;

public class UsersRepository(ApplicationDbContext dbContext)
{
    public async Task<UserEntity?> AddUser(UserEntity user)
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

    public async Task<UserEntity?> FindUserByEmail(string email)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(user => user.Email == email);
        return user;
    }

    public async Task UpdateUserRefreshToken(UserEntity user)
    {
        await dbContext.Users
            .Where(u => u.Id == user.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.RefreshToken, user.RefreshToken)
                .SetProperty(u => u.RefreshTokenExpiryTime, user.RefreshTokenExpiryTime));
    }

    public async Task UpdateUser(UserEntity user)
    {
        await dbContext.Users
            .Where(u => u.Id == user.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Name, user.Name)
                .SetProperty(u => u.Email, user.Email)
                .SetProperty(u => u.RefreshToken, user.RefreshToken)
                .SetProperty(u => u.RefreshTokenExpiryTime, user.RefreshTokenExpiryTime)
                .SetProperty(u => u.IsAdmin, user.IsAdmin)
                .SetProperty(u => u.RoomId, user.RoomId)
                .SetProperty(u => u.SentTime, user.SentTime)
                .SetProperty(u => u.ProgramWorkingTime, user.ProgramWorkingTime)
                .SetProperty(u => u.CompetitionOverhead, user.CompetitionOverhead));
    }

    public async Task<UserEntity?> GetUserById(Guid userId)
    {
        var user = await dbContext.Users
            .FindAsync(userId);
        return user;
    }
    
    public async Task<List<UserEntity>> GetUsersByRoomId(Guid roomId)
    {
        return await dbContext.Users.Where(u => u.RoomId == roomId).ToListAsync();
    }
    
    public async Task<List<UserEntity>> GetUsersByRoomIdInOrderByKey<TKey>(Guid roomId, 
        Expression<Func<UserEntity, TKey>> key)
    {
        return await dbContext.Users.Where(u => u.RoomId == roomId)
            .OrderBy(key)
            .ToListAsync();
    }
}