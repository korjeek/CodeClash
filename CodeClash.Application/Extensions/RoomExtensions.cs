using CodeClash.Core.Models;
using CodeClash.Core.Models.Domain;
using CodeClash.Core.Models.DTOs;
using CodeClash.Persistence.Entities;

namespace CodeClash.Application.Extensions;

public static class RoomExtensions
{
    public static RoomDTO GetRoomDtoFromRoom(this Room room) => new RoomDTO
    {
        Id = room.Id.ToString(),
        Name = room.Name,
        Time = room.Time,
        IssueName = room.Issue.Name,
        Users = room.Participants.Select(u => u.GetUserDto()).ToList()
    };

    public static RoomDTO GetRoomDtoFromRoomEntity(this RoomEntity roomEntity) => new RoomDTO
    {
        Id = roomEntity.Id.ToString(),
        Name = roomEntity.Name,
        Time = roomEntity.Time,
        ParticipantsCount = roomEntity.ParticipantsCount
    };

    public static RoomEntity GetRoomEntity(this Room room) => new RoomEntity
    {
        Id = room.Id,
        Name = room.Name,
        Time = room.Time,
        IssueId = room.Issue.Id
    };

    public static Room GetRoomFromEntity(this RoomEntity roomEntity, Issue issue) => Room.Create(
        roomEntity.Id,
        roomEntity.Name,
        roomEntity.Time,
        issue
    ).Value;
}