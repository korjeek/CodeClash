using CodeClash.Persistence.Repositories;
using CSharpFunctionalExtensions;

namespace CodeClash.Application.Services;

public class UserService(RoomsRepository roomsRepository, UsersRepository usersRepository)
{
    public async Task<Guid?> GetUserRoomId(Guid userId)
    {
        var userEntity = await usersRepository.GetUserById(userId);
        return userEntity?.RoomId;
    }
}