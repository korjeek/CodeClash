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
}