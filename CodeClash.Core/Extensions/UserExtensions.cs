using CodeClash.Core.Models;
using CodeClash.Core.Models.DTOs;

namespace CodeClash.Core.Extensions;

public static class UserExtensions
{
    public static UserDTO GetUserDTO(this User user) => new UserDTO
    {
        Email = user.Email,
        Name = user.Name
    };

    public static UserEntity GetUserEntity(this User user) => new UserEntity
    {
        Id = user.Id,
        Email = user.Email,
        Name = user.Name,
        PasswordHash = user.PasswordHash,
        RefreshToken = user.RefreshToken,
        RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
        IsAdmin = user.IsAdmin
    };

    public static User GetUserFromEntity(this UserEntity userEntity) => User.Create(
        userEntity.Id,
        userEntity.Email,
        userEntity.PasswordHash,
        userEntity.Name,
        userEntity.IsAdmin
    ).Value;
}