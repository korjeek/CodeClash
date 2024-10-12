using CodeClash.Core.Models;

namespace ClashCode.Application.Interfaces.Auth;

public interface IJwtProvider
{
    string GenerateToken(User user);
}