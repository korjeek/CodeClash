namespace CodeClash.Persistence.Entities;

public class RoomEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public TimeOnly Time { get; set; }
    public RoomStatus Status { get; set; }
    public Guid IssueId { get; set; }
}