using System.ComponentModel.DataAnnotations;
using CodeClash.Persistence.Interfaces;

namespace CodeClash.Core.Models;

public class RoomEntity : IEntity
{
    public Guid Id { get; init; }

    [Required] 
    public TimeOnly Time { get; init; }
    public RoomStatus Status { get; set; }
    public List<UserEntity> Participants { get; init; } = null!;

    [Required] 
    public IssueEntity IssueEntity { get; init; } = null!;
    public Guid IssueId { get; init; }

    private RoomEntity()
    {
    }

    public RoomEntity(TimeOnly time, IssueEntity issueEntity)
    {
        Time = time;
        IssueEntity = issueEntity;
        Participants = [];
    }
    
    
    public enum RoomStatus
    {
        WaitingForParticipants = 0,
        CompetitionInProgress = 1
    }
}