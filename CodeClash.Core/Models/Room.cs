using System.ComponentModel.DataAnnotations;

namespace CodeClash.Core.Models;

public class Room
{
    public Guid Id { get; init; }

    [Required] public TimeOnly Time { get; init; }
    public RoomStatus Status { get; set; }
    public List<User> Participants { get; init; } = null!;

    [Required] 
    public Issue Issue { get; init; } = null!;
    public Guid IssueId { get; init; }

    private Room()
    {
    }

    public Room(TimeOnly time, Issue issue)
    {
        Time = time;
        Issue = issue;
        Participants = [];
    }
    
    
    public enum RoomStatus
    {
        WaitingForParticipants = 0,
        CompetitionInProgress = 1
    }
}