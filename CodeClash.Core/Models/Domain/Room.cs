using System.ComponentModel.DataAnnotations;
using CodeClash.Core.Interfaces;

namespace CodeClash.Core.Models;

public class Room : IModel<RoomEntity>
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public RoomEntity GetEntity()
    {
        throw new NotImplementedException();
    }

    public TimeOnly Time { get; init; }

    public List<string> Participants { get; init; }

    public Issue Issue { get; init; }

    public Room Create(TimeOnly timeOnly, Issue issue)
    {
        return new Room(timeOnly, issue);
    }

    public RoomEntity ModelToEntity()
    {
        throw new NotImplementedException();
    }

    private Room(TimeOnly time, Issue issue)
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