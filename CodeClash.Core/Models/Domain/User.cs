
using System.ComponentModel.DataAnnotations;
using CodeClash.Core.Interfaces;

namespace CodeClash.Core.Models;

public class User(string name, string email, string passwordHash) : IModel<UserEntity>
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Email { get; init; } = email;

    public string PasswordHash { get; set; } = passwordHash;

    public string Name { get; set; } = name;

    public DateTime RefreshTokenExpiryTime { get; set; }

    public string? RefreshToken { get; set; }

    public UserEntity GetEntity()
    {
        return new UserEntity();
    }
}