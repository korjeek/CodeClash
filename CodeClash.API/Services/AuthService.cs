using System.Security.Claims;
using CodeClash.Application;
using CodeClash.Core.Models;
using CodeClash.Core.Models.Identity;
using CodeClash.Core.Services;
using CodeClash.Persistence.Repositories;

namespace CodeClash.API.Services;

public class AuthService(UsersRepository usersRepository, TokenService tokenService)
{
    
    //TODO: Переписать сигнатуру методов. Нехорошо, что они принимают реквесты, они должны быть только в Controllers
    public async Task<UserEntity?> GetUser(LoginRequest request)
    {
        var user = await usersRepository.FindUserByEmail(request.Email);
        if (user is null)
            return null;

        return PasswordHasher.Verify(request.Password, user.PasswordHash) ? user : null;
    }

    public async Task<User?> CreateUser(RegisterRequest request)
    {
        var passwordHash = PasswordHasher.Generate(request.Password);
        var newUser = new User(request.UserName, request.Email, passwordHash);
        return await usersRepository.AddUser(newUser);
    }

    public async Task<UserEntity?> GetUserByPrincipalClaims(JwtToken tokenModel)
    {
        var principal = tokenService.GetPrincipalClaims(tokenModel.AccessToken);
        
        if (principal is null)
            return null;
        return await usersRepository.FindUserByEmail(principal.Claims
            .First(claim => claim.Type == ClaimTypes.Email).Value);
    }

    public async Task<JwtToken> UpdateUsersTokens(UserEntity userEntity)
    {
        var tokens = tokenService.UpdateTokens(userEntity);
        UpdateUsersRefreshTokenProperties(userEntity, tokens.RefreshToken);
        await usersRepository.UpdateUserRefreshToken(userEntity);
        
        return tokens;
    }

    private void UpdateUsersRefreshTokenProperties(UserEntity userEntity, string refreshToken)
    {
        userEntity.RefreshToken = refreshToken;
        userEntity.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(12);
    }
}