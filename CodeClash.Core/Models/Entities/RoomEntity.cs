using System.ComponentModel.DataAnnotations;
using CodeClash.Core.Models.Enums;
using CodeClash.Persistence.Interfaces;

namespace CodeClash.Core.Models;

public class RoomEntity : IEntity
{
    public Guid Id { get; init; }
    public string Name { get; set; }

    [Required] 
    public TimeOnly Time { get; set; }
    public RoomStatus Status { get; set; }
    public List<UserEntity> Participants { get; set; } = null!;

    [Required] 
    public IssueEntity IssueEntity { get; set; } = null!;
    public Guid IssueId { get; set; }
}