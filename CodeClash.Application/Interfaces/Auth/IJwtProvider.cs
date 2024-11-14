using CodeClash.Core.Models;

namespace ClashCode.Application.Interfaces.Auth;

public interface IJwtProvider
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken(User user);
}