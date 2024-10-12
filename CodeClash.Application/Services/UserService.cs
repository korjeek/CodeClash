using ClashCode.Application.Interfaces.Auth;
using CodeClash.Application.Interfaces.Repositories;
using CodeClash.Core.Models;

namespace CodeClash.Application.Services;

public class UserService(IPasswordHasher passwordHasher, IUsersRepositories usersRepositories, IJwtProvider jwtProvider)
{
    public async Task Register(string username, string email, string password)
    {
        var hashedPassword = passwordHasher.Generate(password);
        var user = new User(Guid.NewGuid(), username, hashedPassword, email);
        await usersRepositories.Add(user);
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await usersRepositories.GetByEmail(email);
        if (passwordHasher.Verify(password, user.Password))
            throw new Exception();
        
        return jwtProvider.GenerateToken(user);
    }
}