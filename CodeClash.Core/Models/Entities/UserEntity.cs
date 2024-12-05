
using System.ComponentModel.DataAnnotations;
using CodeClash.Persistence.Interfaces;

namespace CodeClash.Core.Models;

public class UserEntity : IEntity
{
    public Guid Id { get; init; }
    
    [Required]
    [MaxLength(320)]
    public string Email { get; init; }
    [Required]
    public string PasswordHash { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    
    [Required]
    [MaxLength(128)]
    public string Name { get; set; }
    public string? RefreshToken { get; set; }
    
    public RoomEntity? Room { get; set; }
    public Guid? RoomId { get; set; }
    public bool IsAdmin { get; set; }
    

    public UserEntity(string name, string email, string passwordHash)
    {
        Email = email;
        Name = name;
        PasswordHash = passwordHash;
    }
}