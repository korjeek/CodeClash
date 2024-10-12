using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClashCode.Application.Interfaces.Auth;
using CodeClash.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CodeClash.Infrastructure;

public record JwtOptions(string SecretKey, int ExpiresMinutes);

public class JwtProvider(IOptions<JwtOptions> options): IJwtProvider
{
    private readonly JwtOptions _options = options.Value;
    
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new("userId", user.Id.ToString())
        };
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddMinutes(_options.ExpiresMinutes));
        
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenValue;
    }
}