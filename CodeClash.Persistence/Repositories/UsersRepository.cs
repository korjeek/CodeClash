using CodeClash.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence.Repositories;
// TODO: подумать над тем, а нужны ли нам вообще в репозиториях эти Result<>?
public class UsersRepository(ApplicationDbContext dbContext)
{
    // TODO: Подумать, можно ли в этом метде обойтись без Result?
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

    public async Task UpdateUser(UserEntity user, Guid? roomId = null)
    {
        await dbContext.Users
            .Where(u => u.Id == user.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Name, user.Name)
                .SetProperty(u => u.Email, user.Email)
                .SetProperty(u => u.RefreshToken, user.RefreshToken)
                .SetProperty(u => u.RefreshTokenExpiryTime, user.RefreshTokenExpiryTime)
                .SetProperty(u => u.IsAdmin, user.IsAdmin)
                .SetProperty(u => u.RoomId, roomId));
    }

    public async Task<UserEntity?> GetUserById(Guid userId)
    {
        var user = await dbContext.Users
            .FindAsync(userId);
        return user;
    }
}