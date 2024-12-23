namespace CodeClash.Persistence.Entities;

public class RoomEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public TimeOnly Time { get; set; }
    // public int ParticipantsCount { get; set; } TODO: фича, которая может быть будет скоро))
    public RoomStatus Status { get; set; }
    public Guid IssueId { get; set; }
}