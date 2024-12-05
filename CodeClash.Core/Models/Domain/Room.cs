using CodeClash.Core.Interfaces;

namespace CodeClash.Core.Models;

public class Room(TimeOnly time, string issue, string name) : IModel<RoomEntity>
{
    public Guid Id { get; } = Guid.NewGuid();

    public TimeOnly Time { get; init; } = time;

    public string Name { get; set; } = name;

    public string IssueName { get; init; } = issue;

    public static Room GetModel(RoomEntity roomEntity)
    {
        return new Room(
            roomEntity.Time,
            Issue.GetModel(roomEntity.IssueEntity).Name,
            roomEntity.Name);
    }

    public RoomEntity GetEntity()
    {
        throw new NotImplementedException();
    }
}