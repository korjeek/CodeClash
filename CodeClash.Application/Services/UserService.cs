using CodeClash.Persistence.Repositories;
using CSharpFunctionalExtensions;

namespace CodeClash.Application.Services;

public class UserService(UsersRepository usersRepository)
{
    public async Task<Result<Guid>> GetUserRoomId(Guid userId)
    {
        var userEntity = await usersRepository.GetUserById(userId);
        return userEntity?.RoomId is null ? Result.Failure<Guid>("User is not in room.") : Result.Success(userEntity.RoomId.Value);
    }
}