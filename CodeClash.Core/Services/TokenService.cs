using System.IdentityModel.Tokens.Jwt;
using CodeClash.Core.Extensions;
using CodeClash.Core.Models;
using Microsoft.Extensions.Configuration;

namespace CodeClash.Core.Services;

public class TokenService(IConfiguration configuration)
{
    public string CreateToken(User user)
    {
        var token = user.CreateClaims().CreateJwtToken(configuration);
        var tokenHandler = new JwtSecurityTokenHandler();
        
        return tokenHandler.WriteToken(token);
    }

    public string UpdateRefreshToken(User user)
    {
        user.RefreshToken = configuration.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(configuration.GetSection("some").Get<int>());
        return user.RefreshToken;
    }
        
}