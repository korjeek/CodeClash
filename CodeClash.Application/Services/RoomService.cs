using System.Reflection;
using CodeClash.Application.Extensions;
using CodeClash.Core.Models.Domain;
using CodeClash.Core.Models.DTOs;
using CodeClash.Persistence.Entities;
using CodeClash.Persistence.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.SignalR;

namespace CodeClash.Application.Services;

public class RoomService(RoomsRepository roomsRepository, IssuesRepository issuesRepository, UsersRepository usersRepository)
{
    public async Task<Result<Room>> CreateRoom(string roomName, TimeOnly time, Guid issueId, Guid userId)
    {
        var issue = await issuesRepository.GetIssueById(issueId);
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
        
        newRoomResult.Value.AddParticipant(adminUser.GetUserFromEntity());

        return newRoomResult;
    }
    
    public async Task<Result<Room>> JoinRoom(Guid roomId, Guid userId)
    {
        var roomEntity = await roomsRepository.GetRoomById(roomId);
        if (roomEntity is null)
            return Result.Failure<Room>($"Room with {roomId} id does not exist.");

        var userEntity = await usersRepository.GetUserById(userId);
        if (userEntity is null)
            return Result.Failure<Room>($"User with {userId} id does not exist.");

        if (userEntity.RoomId is not null)
            return Result.Failure<Room>("User is already in room.");
        
        userEntity.RoomId = roomId;
        await usersRepository.UpdateUser(userEntity);

        var issue = await issuesRepository.GetIssueById(roomEntity.IssueId);
        var roomParticipants = await roomsRepository.GetRoomUsers(roomId);
        var room = roomEntity.GetRoomFromEntity(issue!.GetIssueFromEntity());
        room.SetParticipants(roomParticipants.Select(p => p.GetUserFromEntity()).ToList());
        return Result.Success(room);
    }
    

    public async Task<Result<List<RoomDTO>>> GetAllWaitingRoomDTOs()
    {
        var roomEntities = await roomsRepository.GetRooms();
        return Result.Success(roomEntities
            .Where(r => r.Status == RoomStatus.WaitingForParticipants)
            .Select(r => r.GetRoomDTOFromRoomEntity()).ToList());
    }
    
    public async Task<Result<string>> QuitRoom(Guid userId, Guid roomId, HubCallerContext hubCallerContext, IGroupManager groupManager)
    {
        var userEntity = await usersRepository.GetUserById(userId);
        if (userEntity is null)
            return Result.Failure<string>($"User with {userId} id does not exist");
        if (!userEntity.IsAdmin)
        {
            userEntity.RoomId = null;
            await usersRepository.UpdateUser(userEntity);
            await groupManager.RemoveFromGroupAsync(hubCallerContext.ConnectionId, roomId.ToString());
        }
        else
        {
            userEntity.IsAdmin = false;
            await usersRepository.UpdateUser(userEntity);
            var roomEntity = await roomsRepository.GetRoomById(roomId);
            if (roomEntity is null)
                return Result.Failure<string>("Room does not exist.");
            await roomsRepository.Delete(roomEntity);
        }

        return Result.Success("Quited room successfully.");
    }

    public async Task<Result<bool>> IsUserAdmin(Guid userId)
    {
        var userEntity = await usersRepository.GetUserById(userId);
        if (userEntity is null)
            return Result.Failure<bool>($"User with {userId} does not exist.");
        return Result.Success(userEntity.IsAdmin);
    }
    
    public async Task<Result<Room>> GetRoom(Guid roomId)
    {
        var roomEntity = await roomsRepository.GetRoomById(roomId);
        if (roomEntity is null)
            return Result.Failure<Room>("Room does not exist.");
        
        var issue = (await issuesRepository.GetIssueById(roomEntity.IssueId))!.GetIssueFromEntity();
        var room = roomEntity.GetRoomFromEntity(issue);

        var participants = (await roomsRepository.GetRoomUsers(roomId))
            .Select(u => u.GetUserFromEntity())
            .ToList();
        room.SetParticipants(participants);
        
        return Result.Success(room);
    }
}