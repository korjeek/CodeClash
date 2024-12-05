using CodeClash.Core.Models;
using CodeClash.Core.Models.RoomsRequests;
using CodeClash.Persistence.Repositories;

namespace CodeClash.API.Services;

public class RoomService(RoomsRepository roomsRepository, IssuesRepository issuesRepository, UsersRepository usersRepository)
{
    public async Task<RoomEntity?> CreateRoom(TimeOnly time, Guid issueId, Guid userId)
    {
        var issue = await issuesRepository.GetIssueById(issueId); // Guid.Parse(request.IssueId)
        if (issue == null)
            return null;
        
        return await roomsRepository.Add(new RoomEntity(time, issue), userId);
    }
    
    public async Task<RoomEntity?> JoinRoom(Guid roomId, Guid userId)
    {
        return await roomsRepository.AddUserToRoom(userId, roomId);
    }
    
    public async Task<RoomEntity?> QuitRoom(Guid roomId, Guid userId)
    {
        return await roomsRepository.RemoveUserFromRoom(userId, roomId);
    }

    public async Task<RoomEntity?> CloseRoom(Guid roomId)
    {
        throw new NotImplementedException();
    }
    
    
    
    
    
    
    public async Task<RoomEntity?> StartCompetition(Guid roomId)
    {
        var room = await roomsRepository.GetRoomById(roomId);
        if (room == null)
            return null;
        if (room.Status == RoomEntity.RoomStatus.CompetitionInProgress) return null;
        room.Status = RoomEntity.RoomStatus.CompetitionInProgress;
        return room;
    }

    public async Task<RoomEntity?> FinishCompetition(Guid roomId)
    {
        var room = await roomsRepository.GetRoomById(roomId);
        if (room == null)
            return null;
        if (room.Status == RoomEntity.RoomStatus.WaitingForParticipants) return null;
        room.Status = RoomEntity.RoomStatus.WaitingForParticipants;
        return room;
    }
}