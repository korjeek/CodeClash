
namespace CodeClash.Core.Models;

public class UserEntity
{
    public Guid Id { get; init; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Name { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string? RefreshToken { get; set; }
    public bool IsAdmin { get; set; }
    public Guid? RoomId { get; set; }
}