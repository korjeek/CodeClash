namespace CodeClash.Core.Models;

public class Room
{
    public Guid Id { get; }
    public Issue Issue { get; }
    public TimeOnly Time { get; }
    public RoomStatus Status { get; set; }
    public List<User> Participants { get; }
    public Guid AdminId { get; set; }
    public User Admin { get; set; }
    
    public Room(){}

    public Room(TimeOnly time, Issue issue, User admin)
    {
        Id = Guid.NewGuid();
        Time = time;
        Issue = issue;
        Participants = new List<User>();
        Status = RoomStatus.WaitingForParticipants;
        Admin = admin;
        AdminId = admin.Id;
    }
    
    
    public enum RoomStatus
    {
        WaitingForParticipants = 0,
        CompetitionInProgress = 1
    }
}