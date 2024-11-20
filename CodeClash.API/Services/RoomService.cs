using System.Text.RegularExpressions;
using CodeClash.Core.Models;
using CodeClash.Persistence.Repositories;

namespace CodeClash.API.Services;

public class RoomService(RoomsRepository repository)
{
    public async Task<string> CreateRoom(TimeOnly time, Issue issue)
    {
        return await repository.Add(new Room(time, issue));
    }
    
    public async Task<Room?> EnterRoom(Guid roomId, User participant)
    {
        var room = await repository.GetRoomById(roomId);
        if (room == null)
            return null;
        if (room.Status == Room.RoomStatus.WaitingForParticipants)
            room.Participants.Add(participant);
        else
            return null;
        return room; // нужно ли возвращать комнату?
    }
    
    public async Task<Room?> QuitRoom(Guid roomId, User participant)
    {
        var room = await repository.GetRoomById(roomId);
        if (room == null)
            return null;
        return room.Participants.Remove(participant) ? room : null;
    }
    
    public async Task<Room?> StartCompetition(Guid roomId)
    {
        var room = await repository.GetRoomById(roomId);
        if (room == null)
            return null;
        if (room.Status == Room.RoomStatus.CompetitionInProgress) return null;
        room.Status = Room.RoomStatus.CompetitionInProgress;
        return room;
    }
    
    
}