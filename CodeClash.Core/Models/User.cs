
namespace CodeClash.Core.Models;

public class User(string userName, string email, string passwordHash)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserName { get; set; } = userName;
    public string Email { get; set; } = email;
    public string PasswordHash { get; set; } = passwordHash;
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}