using CodeClash.Core.Models;
using CodeClash.Core.Models.RoomsRequests;
using CodeClash.Persistence.Repositories;

namespace CodeClash.API.Services;

public class RoomService(RoomsRepository roomsRepository, IssuesRepository issuesRepository, UsersRepository usersRepository)
{
    public async Task<Room?> CreateRoom(TimeOnly time, Guid issueId, string userEmail)
    {
        var issue = await issuesRepository.GetIssueById(issueId); // Guid.Parse(request.IssueId)
        if (issue == null)
            return null;
        
        return await roomsRepository.Add(new Room(time, issue), userEmail);
    }
    
    public async Task<Room?> EnterRoom(EnterQuitRoomRequest request)
    {
        var participant = await usersRepository.FindUserByEmail(request.UserEmail);
        return await roomsRepository.AddUserToRoom(participant!, request.RoomId);
    }
    
    public async Task<Room?> QuitRoom(EnterQuitRoomRequest request)
    {
        var user = await usersRepository.FindUserByEmail(request.UserEmail);
        return await roomsRepository.RemoveUserFromRoom(user!, request.RoomId);
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