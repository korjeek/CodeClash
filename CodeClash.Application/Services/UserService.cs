using CodeClash.Persistence.Repositories;

namespace CodeClash.Application.Services;

public class UserService(UsersRepository usersRepository)
{
    public async Task<Guid?> GetUserRoomId(Guid userId)
    {
        var userEntity = await usersRepository.GetUserById(userId);
        return userEntity?.RoomId;
    }
}