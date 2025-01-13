using CSharpFunctionalExtensions;
using static CodeClash.Core.Constants.Constants;


namespace CodeClash.Core.Models;
public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set;}
    public string PasswordHash { get; private set;} 
    public string Name { get; private set; }
    public DateTime RefreshTokenExpiryTime { get; private set; }
    public string? RefreshToken { get; private set; }
    
    public bool IsAdmin { get; private set; }
    
    private User(Guid id, string email, string passwordHash, string name, bool isAdmin)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        Name = name;
        IsAdmin = isAdmin;
    }

    public static Result<User> Create(Guid id, string email, string passwordHash, string name, bool isAdmin = false)
    {
        if (string.IsNullOrWhiteSpace(email) || email.Length > MaxEmailLength)
            return Result.Failure<User>("Incorrect email length.");
        if (string.IsNullOrWhiteSpace(name) || name.Length > MaxNameLength)
            return Result.Failure<User>("Incorrect name length.");
        
        return Result.Success(new User(id, email, passwordHash, name, isAdmin));
    }

    public void SetRoomAdminStatus(bool isAdmin) => IsAdmin = isAdmin;
    
    public void SetRefreshToken(string? refreshToken) => RefreshToken = refreshToken;

    public void SetRefreshTokenExpiryTime(DateTime refreshTokenExpiryTime) =>
        RefreshTokenExpiryTime = refreshTokenExpiryTime;
}