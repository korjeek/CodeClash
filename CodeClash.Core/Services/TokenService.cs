using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CodeClash.Core.Extensions;
using CodeClash.Core.Models;
using CodeClash.Core.Models.Identity;
using Microsoft.Extensions.Configuration;

namespace CodeClash.Core.Services;

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
        user.RefreshToken = configuration.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(configuration.GetSection("Jwt:RefreshTokenValidityInDays").Get<int>());
        return user.RefreshToken;
    }

    public ClaimsPrincipal? GetPrincipalClaims(string accessToken) => configuration.GetPrincipalFromExpiredToken(accessToken);

    public JwtToken CreateTokensByPrincipleClaims(List<Claim> claims)
    {
        var newAccessToken = CompileJwtSecurityToken(configuration.CreateToken(claims));
        var newRefreshToken = configuration.GenerateRefreshToken();
        return new JwtToken(newAccessToken, newRefreshToken);
    }
}