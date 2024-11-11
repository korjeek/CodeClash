using CodeClash.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace CodeClash.Application.Services;

public class UserService(UserManager<User> userManager)
{
    public async Task<bool> IsLoginValid(string email, string password)
    {
        var managedUser = await userManager.FindByEmailAsync(email);
        if (managedUser == null)
            return false;
        
        var managedPassword = await userManager.CheckPasswordAsync(managedUser , password);
        if (!managedPassword)
            return false;

        return true;
    }

    public async Task<User?> FindUserByUsername(string? username)
    {
        if (username == null)
            return null;
        
        return await userManager.FindByNameAsync(username);
    }

    public async void UpdateRefreshToken(User user, string refreshToken)
    {
        user.RefreshToken = refreshToken;
        await userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> CreateUser(string username, string email)
    {
        var user = new User
        {
            UserName = username,
            Email = email
        };
        
        return await userManager.CreateAsync(user);
    }
}