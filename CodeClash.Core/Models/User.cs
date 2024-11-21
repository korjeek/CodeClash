
namespace CodeClash.Core.Models;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string Name { get; set; }
    public string? RefreshToken { get; set; }
    
    public Room Room { get; set; }
    public Guid? RoomId { get; set; }
    public bool IsAdmin { get; set; }

    public User(string name, string email, string passwordHash)
    {
        Id = Guid.NewGuid();
        Email = email;
        Name = name;
        PasswordHash = passwordHash;
    }
}