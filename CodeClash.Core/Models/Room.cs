using Microsoft.IdentityModel.Tokens;

namespace CodeClash.Core.Models;

public class Room(TimeOnly time, Issue issue, User admin)
{
    public Guid Id { get; } = Guid.NewGuid();
    public Issue Issue { get; } = issue;
    public TimeOnly Time { get; } = time;
    public RoomStatus Status { get; set; } = RoomStatus.WaitingForParticipants;
    public List<User> Participants { get; } = [];
    public User Admin { get; set; } = admin;
    
    public enum RoomStatus
    {
        WaitingForParticipants = 0,
        CompetitionInProgress = 1
    }
}