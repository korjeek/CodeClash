using System.Security.Claims;
using CodeClash.Application.Extensions;
using CodeClash.Core.Models;
using CodeClash.Core.Models.Identity;
using CodeClash.Persistence.Repositories;
using CSharpFunctionalExtensions;

namespace CodeClash.Application.Services;

public class AuthService(UsersRepository usersRepository, TokenService tokenService)
{
    public async Task<Result<User>> CreateUser(string name, string email, string password)
    {
        var passwordHash = PasswordHasher.Generate(password);
        var newUserResult = User.Create(Guid.NewGuid(), email, passwordHash, name);
        if (newUserResult.IsFailure)
            return Result.Failure<User>(newUserResult.Error);
        var user = await usersRepository.AddUser(newUserResult.Value.GetUserEntity());
        return user is null
            ? Result.Failure<User>($"User with {email} email already exists.")
            : Result.Success(user.GetUserFromEntity());
    }

    public async Task<Result<User>> GetVerifiedUser(string email, string password)
    {
        var user = await usersRepository.FindUserByEmail(email);
        if (user is null)
            return Result.Failure<User>($"User with {email} email does not exist.");

        return PasswordHasher.Verify(password, user.PasswordHash)
            ? Result.Success(user.GetUserFromEntity())
            : Result.Failure<User>("Password is incorrect.");
    }

    public async Task<JwtToken> UpdateUserTokens(User user)
    {
        var tokens = tokenService.UpdateTokens(user);
        UpdateUsersRefreshTokenProperties(user, tokens.RefreshToken);
        await usersRepository.UpdateUserRefreshToken(user.GetUserEntity());

        return tokens;
    }

    public async Task<Result<User>> GetUserByPrincipalClaims(JwtToken tokenModel)
    {
        var principal = tokenService.GetPrincipalClaims(tokenModel.AccessToken);

        if (principal is null)
            return Result.Failure<User>("Complex refresh token error is occured.");
        var user = await usersRepository.FindUserByEmail(principal.Claims
            .First(claim => claim.Type == ClaimTypes.Email).Value);
        return user is null
            ? Result.Failure<User>($"User with email {ClaimTypes.Email} does not exits.")
            : Result.Success(user.GetUserFromEntity());
    }

    private void UpdateUsersRefreshTokenProperties(User user, string refreshToken)
    {
        user.UpdateRefreshToken(refreshToken);
        user.UpdateRefreshTokenExpiryTime(DateTime.UtcNow.AddHours(12));
    }
}