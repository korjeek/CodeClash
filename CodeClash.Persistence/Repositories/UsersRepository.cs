using CodeClash.Application.Interfaces.Repositories;
using CodeClash.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CodeClash.Persistence.Repositories;

public class UsersRepository(IdentityDbContext<User> context)
{
    public async Task Add(User user)
    {
        throw new NotImplementedException();
    }

    public User? FindUserByEmail(string email)
    {
        var user = context.Users.FirstOrDefault(user => user.Email == email);
        context.SaveChangesAsync();
        
        return user;
    }
}