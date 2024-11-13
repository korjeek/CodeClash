using CodeClash.Application;
using CodeClash.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence.Repositories;

public class UsersRepository(ApplicationDbContext dbContext)
{
    public async Task<User?> AddUser(User user)
    {
        //TODO: Хочу получать создавать пользователя в БД,
        //TODO: а затем получать инфу успешно это или нет (либо User = null или че то другое (т.е. не User, что удобно))
        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<User?> FindUserByUserName(string? userName)
    {
        //TODO: Хочу получать пользователя по никнейму в бд, если нет -> null
    }

    public async Task<User?> FindUserByEmail(string email)
    {
        return await dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == email);
    }

    public async void UpdateUsersRefreshToken(User user, string newRefreshToken)
    {
        //TODO: Хочу обновить RefreshToken у пользователя
        user.RefreshToken = newRefreshToken;
    }

    public async Task<string> GetPassword(User user)
    {
        //TODO: Хочу получить хэшированный пароль пользователя
    }
}