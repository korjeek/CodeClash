using CodeClash.Persistence.Repositories;

namespace CodeClash.Application.Services;

public class AuthService(UsersRepository usersRepository, PasswordHasher passwordHasher)
{
    public async Task<bool> IsLoginValid(string email, string password)
    {
        var managedUser = await usersRepository.FindUserByEmail(email);
        if (managedUser == null)
            return false;
        
        var managedPassword = await usersRepository.GetPassword(managedUser.Id);
        return passwordHasher.Verify(password, managedPassword);
    }
    
    public async Task<string> HashPassword(string password) => passwordHasher.Generate(password);
}