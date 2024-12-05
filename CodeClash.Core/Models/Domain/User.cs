using System.ComponentModel.DataAnnotations;
using CodeClash.Core.Interfaces;

namespace CodeClash.Core.Models;

public class User(string name, string email, string passwordHash, DateTime refreshTokenExpiryTime, string refreshToken) : IModel<UserEntity>
{
    public Guid Id { get; } = Guid.NewGuid();

    public string Email { get; } = email;

    public string PasswordHash { get; } = passwordHash;

    public string Name { get; } = name;

    public DateTime RefreshTokenExpiryTime { get; set; }

    public string? RefreshToken { get; set; }

    public UserEntity GetEntity()
    {
        return new UserEntity
        {
            Id = Id,
            Name = Name,
            Email = Email,
            PasswordHash = PasswordHash,
            RefreshTokenExpiryTime = RefreshTokenExpiryTime,
            RefreshToken = RefreshToken
        };
    }

    public static User GetModel(UserEntity userEntity)
    {
        return new User(
            userEntity.PasswordHash,
            userEntity.Email,
            userEntity.PasswordHash,
            userEntity.RefreshTokenExpiryTime,
            userEntity.RefreshToken);
    }
}