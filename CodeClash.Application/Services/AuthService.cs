using System.Security.Claims;
using CodeClash.Application.Extensions;
using CodeClash.Core.Models;
using CodeClash.Core.Models.Identity;
using CodeClash.Persistence.Repositories;
using CSharpFunctionalExtensions;

namespace CodeClash.Application.Services;

public class AuthService(UsersRepository usersRepository, TokenService tokenService)
{
    
    //TODO: Переписать сигнатуру методов. Нехорошо, что они принимают реквесты, они должны быть только в Controllers
    public async Task<Result<User>> GetUser(string email, string password)
    {
        var user = await usersRepository.FindUserByEmail(email);
        if (user is null)
            return Result.Failure<User>($"User with {email} email does not exist.");

        return PasswordHasher.Verify(password, user.PasswordHash) ? 
            Result.Success(user.GetUserFromEntity()) : Result.Failure<User>("Password is incorrect.");
    }

    public async Task<Result<User>> CreateUser(string name, string email, string password)
    {
        var passwordHash = PasswordHasher.Generate(password);
        var newUserResult = User.Create(Guid.NewGuid(), email, passwordHash, name);
        if (newUserResult.IsFailure)
            return Result.Failure<User>(newUserResult.Error);
        var user = await usersRepository.AddUser(newUserResult.Value.GetUserEntity());
        if (user is null)
            return Result.Failure<User>($"User with {email} email already exists.");
        return Result.Success(user.GetUserFromEntity());
    }

    public async Task<Result<User>> GetUserByPrincipalClaims(JwtToken tokenModel)
    {
        var principal = tokenService.GetPrincipalClaims(tokenModel.AccessToken);
        
        if (principal is null)
            return Result.Failure<User>("Complex refresh token error is occured.");
        var user = await usersRepository.FindUserByEmail(principal.Claims
            .First(claim => claim.Type == ClaimTypes.Email).Value);
        if (user is null)
            return Result.Failure<User>($"EMAIL НЕТУ");
        return Result.Success(user.GetUserFromEntity());
    }

    public async Task<JwtToken> UpdateUsersTokens(User user)
    {
        var tokens = tokenService.UpdateTokens(user);
        UpdateUsersRefreshTokenProperties(user, tokens.RefreshToken);
        await usersRepository.UpdateUserRefreshToken(user.GetUserEntity());
        
        return tokens;
    }

    private void UpdateUsersRefreshTokenProperties(User user, string refreshToken)
    {
        user.UpdateRefreshToken(refreshToken);
        user.UpdateRefreshTokenExpiryTime(DateTime.UtcNow.AddHours(12));
    }
}