namespace CodeClash.Core.Models.DTOs;

public class UserDTO
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string? SentTime { get; set; }
    public string? ProgramWorkingTime { get; set; }
    public string? CompetitionOverhead { get; set; }
}