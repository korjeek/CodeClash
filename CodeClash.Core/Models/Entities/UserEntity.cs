
using System.ComponentModel.DataAnnotations;
using CodeClash.Persistence.Interfaces;

namespace CodeClash.Core.Models;

public class UserEntity : IEntity
{
    public Guid Id { get; init; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string Name { get; set; }
    public string? RefreshToken { get; set; }
    public RoomEntity? Room { get; set; }
    public Guid? RoomId { get; set; }
    public bool IsAdmin { get; set; }
}