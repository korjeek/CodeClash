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
        // А КАКОГО ХУЯ, СПРАШИВАЕТСЯ, МЫ УВЕРЕНЫ, ЧТО ЗДЕСЬ ДОЛЖЕН ИСКАТЬСЯ ADMIN??????
        var admin = await usersRepository.FindUserByEmail(request.UserEmail);
        return await roomsRepository.Add(new Room(request.Time, issue, admin!));
    }
    
    public async Task<Room?> EnterRoom(EnterQuitRoomRequest request)
    {
        var participant = await usersRepository.FindUserByEmail(request.UserEmail);
        return await roomsRepository.AddUserToRoom(participant!, request.RoomId);
    }
    
    public async Task<Room?> QuitRoom(EnterQuitRoomRequest request)
    {
        var user = await usersRepository.FindUserByEmail(request.UserEmail);
        return await roomsRepository.RemoveUserFromRoom(user, request.RoomId);
        // return room.Participants.Remove(participant) ? room : null;
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