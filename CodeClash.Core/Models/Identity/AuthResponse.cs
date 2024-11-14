namespace CodeClash.Core.Models.Identity;

public record AuthResponse(string Username, string Email, string Token, string RefreshToken);
