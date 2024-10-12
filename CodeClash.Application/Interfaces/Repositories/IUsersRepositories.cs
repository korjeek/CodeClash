using CodeClash.Core.Models;

namespace CodeClash.Application.Interfaces.Repositories;

public interface IUsersRepositories
{
    Task Add(User user);
    Task<User> GetByEmail(string email);
}