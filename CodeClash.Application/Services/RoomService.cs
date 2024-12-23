using CodeClash.Application.Extensions;
using CodeClash.Core.Models.Domain;
using CodeClash.Persistence.Entities;
using CodeClash.Persistence.Repositories;
using CSharpFunctionalExtensions;

namespace CodeClash.Application.Services;

public class RoomService(RoomsRepository roomsRepository, IssuesRepository issuesRepository, UsersRepository usersRepository)
{
    public async Task<Result<Room>> CreateRoom(string roomName, TimeOnly time, Guid issueId, Guid userId)
    {
        var issue = await issuesRepository.GetIssueById(issueId); // Guid.Parse(request.IssueId)
        if (issue is null)
            return Result.Failure<Room>("Issue does not exist.");

        var newRoomResult = Room.Create(Guid.NewGuid(), roomName, time, issue.GetIssueFromEntity());
        if (newRoomResult.IsFailure)
            return Result.Failure<Room>(newRoomResult.Error);
        
        var adminUser = await usersRepository.GetUserById(userId);
        if (adminUser is null)
            return Result.Failure<Room>($"User with {userId} id does not exist");
        if (adminUser.IsAdmin)
            return Result.Failure<Room>("User is already admin");
        adminUser.IsAdmin = true;
        
        await roomsRepository.Add(newRoomResult.Value.GetRoomEntity());
        await usersRepository.UpdateUser(adminUser);

        return newRoomResult;
    }
    
    public async Task<Result<Room>> JoinRoom(Guid roomId, Guid userId)
    {
        var roomEntity = await roomsRepository.GetRoomById(roomId);
        if (roomEntity is null)
            return Result.Failure<Room>($"Room with {roomId} id does not exist");

        var user = await usersRepository.GetUserById(userId);
        if (user is null)
            return Result.Failure<Room>($"User with {userId} id does not exist");
        await usersRepository.UpdateUser(user, roomId);

        var issue = await issuesRepository.GetIssueById(roomEntity.IssueId);
        var room = roomEntity.GetRoomFromEntity(issue!.GetIssueFromEntity());
        return Result.Success(room);
    }
    
    public async Task<RoomEntity?> QuitRoom(Guid roomId, Guid userId)
    {
        // return await roomsRepository.RemoveUserFromRoom(userId, roomId);
        throw new NotImplementedException();
    }

    public async Task<RoomEntity?> CloseRoom(Guid roomId)
    {
        throw new NotImplementedException();
    }
    
    
    public async Task<RoomEntity?> StartCompetition(Guid roomId)
    {
        // var room = await roomsRepository.GetRoomById(roomId);
        // if (room == null)
        //     return null;
        // if (room.Status == RoomStatus.CompetitionInProgress) return null;
        // room.Status = RoomStatus.CompetitionInProgress;
        // return room;
        throw new NotImplementedException();
    }

    public async Task<RoomEntity?> FinishCompetition(Guid roomId)
    {
        // var room = await roomsRepository.GetRoomById(roomId);
        // if (room == null)
        //     return null;
        // if (room.Status == RoomStatus.WaitingForParticipants) return null;
        // room.Status = RoomStatus.WaitingForParticipants;
        // return room;
        throw new NotImplementedException();
    }
}