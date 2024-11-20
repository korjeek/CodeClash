using System.Text.RegularExpressions;
using CodeClash.Core.Models;
using CodeClash.Persistence.Repositories;

namespace CodeClash.API.Services;

public class RoomService(RoomsRepository repository)
{
    public async Task<string> CreateRoom(TimeOnly time, Issue issue)
    {
        // TODO: RECHANGE
        return await repository.Add(new Room(time, issue));
    }

    private static bool CheckTime(string time)
    {
        var regex = new Regex(@"^(?:(?:([01]?\d|2[0-3]):)?([0-5]?\d):)?([0-5]?\d)$");
        return regex.IsMatch(time);
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