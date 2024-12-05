using CodeClash.Core.Models;
using CodeClash.Core.Models.Enums;
using CodeClash.Core.Models.RoomsRequests;
using CodeClash.Persistence.Repositories;

namespace CodeClash.API.Services;

public class RoomService(RoomsRepository roomsRepository, IssuesRepository issuesRepository)
{
    public async Task<RoomEntity?> CreateRoom(string roomName,TimeOnly time, Guid issueId, Guid userId)
    {
        var issue = await issuesRepository.GetIssueById(issueId); // Guid.Parse(request.IssueId)
        if (issue == null)
            return null;
        var newRoomEntity = new RoomEntity 
            { 
                Id = Guid.NewGuid(), 
                Name = roomName, 
                Time = time,
                Participants = [],
                IssueEntity = issue
            };
        return await roomsRepository.Add(newRoomEntity, userId);
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
        if (room.Status == RoomStatus.CompetitionInProgress) return null;
        room.Status = RoomStatus.CompetitionInProgress;
        return room;
    }

    public async Task<RoomEntity?> FinishCompetition(Guid roomId)
    {
        var room = await roomsRepository.GetRoomById(roomId);
        if (room == null)
            return null;
        if (room.Status == RoomStatus.WaitingForParticipants) return null;
        room.Status = RoomStatus.WaitingForParticipants;
        return room;
    }
}