using CodeClash.Core.Models;
using CodeClash.Core.Models.DTOs;

namespace CodeClash.Core.Extensions;

public static class RoomExtensions
{
    public static RoomDTO GetRoomDTO(this Room room) => new RoomDTO
    {
        Id = room.Id.ToString(),
        Name = room.Name,
        Users = room.Participants.Select(u => u.GetUserDTO()).ToList()
    };

    public static RoomEntity GetRoomEntity(this Room room) => new RoomEntity
    {
        Id = room.Id,
        Name = room.Name,
        Time = room.Time,
        IssueId = room.Issue.Id
    };
    
    // public static Room GetRoomFromEntity(this RoomEntity roomEntity) => Room.Create(
    //     roomEntity.Id,
    //     roomEntity.Name,
    //     roomEntity.Time,
    //     roomEntity.
    //     )
}