
namespace CodeClash.Persistence.Entities;

public class UserEntity
{
    public Guid Id { get; init; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Name { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string? RefreshToken { get; set; }
    public bool IsAdmin { get; set; }
    public Guid? RoomId { get; set; }
    public bool IsSentSolution { get; set; }
    public TimeOnly? SentTime { get; set; }
    public float? ProgramWorkingTime { get; set; }
    public float? CompetitionOverhead { get; set; } // Чем меньше издержков, тем лучше Типа метрика
}