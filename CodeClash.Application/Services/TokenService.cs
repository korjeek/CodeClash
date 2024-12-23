using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CodeClash.Application.Extensions;
using CodeClash.Core.Models;
using CodeClash.Core.Models.Identity;
using Microsoft.Extensions.Configuration;

namespace CodeClash.Application.Services;

public class TokenService(IConfiguration configuration)
{
    public JwtToken UpdateTokens(User user)
    {
        var accessToken = CreateAccessToken(user);
        var refreshToken = UpdateRefreshToken(user);
        return new JwtToken(accessToken, refreshToken);
    }
    
    private string CreateAccessToken(User user)
    {
        var token = user.CreateClaims().CreateJwtToken(configuration);
        return CompileJwtSecurityToken(token);
    }

    private string CompileJwtSecurityToken(JwtSecurityToken jwtSecurityToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(jwtSecurityToken);
    }

    private string UpdateRefreshToken(User user)
    {
        user.UpdateRefreshToken(JwtBearerExtensions.GenerateRefreshToken());
        user.UpdateRefreshTokenExpiryTime(DateTime.UtcNow.AddDays(7));
        return user.RefreshToken;
    }

    public ClaimsPrincipal? GetPrincipalClaims(string accessToken) => 
        configuration.GetPrincipalFromExpiredToken(accessToken);
}