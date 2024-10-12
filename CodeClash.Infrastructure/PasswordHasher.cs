using ClashCode.Application.Interfaces.Auth;
using Crypt = BCrypt.Net.BCrypt;

namespace CodeClash.Infrastructure;

public class PasswordHasher: IPasswordHasher
{
    public string Generate(string password) => Crypt.EnhancedHashPassword(password);
    
    public bool Verify(string password, string hashedPassword) => Crypt.EnhancedVerify(password, hashedPassword);
}