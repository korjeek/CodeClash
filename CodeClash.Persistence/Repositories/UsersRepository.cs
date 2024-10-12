using CodeClash.Application.Interfaces.Repositories;
using CodeClash.Core.Models;

namespace CodeClash.Persistence.Repositories;

public class UsersRepository: IUsersRepositories
{
    public async Task Add(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetByEmail(string email)
    {
         throw new NotImplementedException();
    }
}