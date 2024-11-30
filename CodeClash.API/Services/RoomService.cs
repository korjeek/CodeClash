using CodeClash.Core.Models;
using CodeClash.Core.Models.RoomsRequests;
using CodeClash.Persistence.Repositories;

namespace CodeClash.API.Services;

public class RoomService(RoomsRepository roomsRepository, IssuesRepository issuesRepository, UsersRepository usersRepository)
{
    public async Task<Room?> CreateRoom(TimeOnly time, Guid issueId, Guid userId)
    {
        var issue = await issuesRepository.GetIssueById(issueId); // Guid.Parse(request.IssueId)
        if (issue == null)
            return null;
        
        return await roomsRepository.Add(new Room(time, issue), userId);
    }
    
    public async Task<Room?> EnterRoom(Guid roomId, Guid userId)
    {
        return await roomsRepository.AddUserToRoom(userId, roomId);
    }
    
    public async Task<Room?> QuitRoom(Guid roomId, Guid userId)
    {
        return await roomsRepository.RemoveUserFromRoom(userId, roomId);
    }

    public async Task<bool> CloseRoom(Guid roomId)
    {
        throw new NotImplementedException();
    }
    
    
    
    
    
    
    public async Task<Room?> StartCompetition(Guid roomId)
    {
        var room = await roomsRepository.GetRoomById(roomId);
        if (room == null)
            return null;
        if (room.Status == Room.RoomStatus.CompetitionInProgress) return null;
        room.Status = Room.RoomStatus.CompetitionInProgress;
        return room;
    }

    public async Task<Room?> FinishCompetition(Guid roomId)
    {
        var room = await roomsRepository.GetRoomById(roomId);
        if (room == null)
            return null;
        if (room.Status == Room.RoomStatus.WaitingForParticipants) return null;
        room.Status = Room.RoomStatus.WaitingForParticipants;
        return room;
    }
}