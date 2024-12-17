using CodeClash.Core.Extensions;
using CodeClash.Core.Models;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence.Repositories;
// TODO: подумать над тем, а нужны ли нам вообще в репозиториях эти Result<>?
public class UsersRepository(ApplicationDbContext dbContext)
{
    // TODO: Подумать, можно ли в этом метде обойтись без Result?
    public async Task<User?> AddUser(User user)
    {
        var userEntity = user.GetUserEntity();
        var isUserEmailContainsInDb = await dbContext.Users
            .Select(u => u.Email)
            .ContainsAsync(userEntity.Email);
        if (isUserEmailContainsInDb)
            return null;
        await dbContext.AddAsync(userEntity);
        await dbContext.SaveChangesAsync();
        
        return user;
    }

    public async Task<User?> FindUserByEmail(string email)
    {
        var userEntity = await dbContext.Users
            .FirstOrDefaultAsync(user => user.Email == email);
        return userEntity?.GetUserFromEntity();
    }

    public async Task UpdateUserRefreshToken(User user)
    {
        var userEntity = user.GetUserEntity();
        await dbContext.Users
            .Where(u => u.Id == userEntity.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.RefreshToken, userEntity.RefreshToken)
                .SetProperty(u => u.RefreshTokenExpiryTime, userEntity.RefreshTokenExpiryTime));
    }

    public async Task UpdateUser(User user, Guid? roomId = null)
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

    public async Task<User?> GetUserById(Guid userId)
    {
        var user = await dbContext.Users
            .FindAsync(userId);
        return user?.GetUserFromEntity();
    }
}