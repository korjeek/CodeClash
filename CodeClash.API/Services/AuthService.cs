using CodeClash.Application;
using CodeClash.Core.Models;
using CodeClash.Core.Models.Identity;
using CodeClash.Core.Services;
using CodeClash.Persistence.Repositories;

namespace CodeClash.API.Services;

public class AuthService(UsersRepository usersRepository, PasswordHasher passwordHasher, TokenService tokenService)
{
    public async Task<User?> GetUser(LoginRequest request)
    {
        var user = await usersRepository.FindUserByEmail(request.Email);
        if (user is null)
            return null;
        
        var password = await usersRepository.GetPassword(user.Id);
        return passwordHasher.Verify(password, user.PasswordHash) ? null : user;
    }

    public async Task<User?> CreateUser(RegisterRequest request)
    {
        var passwordHash = passwordHasher.Generate(request.Password);
        return await usersRepository.AddUser(new User(request.UserName, request.Email, passwordHash));
    }

    public async Task<User?> GetUserByPrincipalClaims(JwtToken tokenModel)
    {
        var principal = tokenService.GetPrincipalClaims(tokenModel.AccessToken);
        return principal is null ? null : await usersRepository.FindUserByUserName(principal.Identity!.Name);
    }

    public JwtToken UpdateUsersTokens(User user)
    {
        var tokens = tokenService.UpdateTokens(user);
        UpdateUsersRefreshTokenProperties(user, tokens.RefreshToken);
        usersRepository.UpdateUsersRefreshToken(user.Id, tokens.RefreshToken);
        
        return tokens;
    }

    private void UpdateUsersRefreshTokenProperties(User user, string refreshToken)
    {
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
    }
}