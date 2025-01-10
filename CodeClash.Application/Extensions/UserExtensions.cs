using CodeClash.Core.Models;
using CodeClash.Core.Models.DTOs;
using CodeClash.Persistence.Entities;


namespace CodeClash.Application.Extensions;

public static class UserExtensions
{
    public static UserDTO GetUserDto(this User user) => new UserDTO
    {
        Email = user.Email,
        Name = user.Name
    };

    public static UserDTO GetUserDto(this UserEntity user) => new UserDTO
    {
        Email = user.Email,
        Name = user.Name,
        SentTime = user.SentTime?.GetTimeStrToSendFront(),
        ProgramWorkingTime = user.ProgramWorkingTime.ToString(),
        CompetitionOverhead = user.CompetitionOverhead.ToString()
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

    public static void ClearUserEntityOverhead(this UserEntity userEntity)
    {
        userEntity.SentTime = null;
        userEntity.CompetitionOverhead = null;
        userEntity.ProgramWorkingTime = null;
    }
}