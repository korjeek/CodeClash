using CodeClash.Core.Models;
using CodeClash.Core.Models.RoomsRequests;
using CodeClash.Persistence.Repositories;

namespace CodeClash.API.Services;

public class RoomService(RoomsRepository roomsRepository, IssuesRepository issuesRepository, UsersRepository usersRepository)
{
    public async Task<Room?> CreateRoom(CreateRoomRequest request)
    {
        var issue = await issuesRepository.GetIssueById(request.IssueId); // Guid.Parse(request.IssueId)
        if (issue == null)
            return null;
        var admin = await usersRepository.FindUserByEmail(request.UserEmail);
        
        return await roomsRepository.Add(new Room(request.Time, issue, admin!));
    }
    
    public async Task<Room?> EnterRoom(EnterRoomRequest request)
    {
        var room = await roomsRepository.GetRoomById(request.RoomId);
        if (room == null)
            return null;
        
        var participant = await usersRepository.FindUserByEmail(request.UserEmail);
        
        if (room.Status == Room.RoomStatus.WaitingForParticipants)
            room.Participants.Add(participant!);
        else
            return null;
        
        return room;
    }
    
    public async Task<Room?> QuitRoom(Guid roomId, User participant)
    {
        var room = await roomsRepository.GetRoomById(roomId);
        if (room == null)
            return null;
        return room.Participants.Remove(participant) ? room : null;
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