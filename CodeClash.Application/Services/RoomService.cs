using CodeClash.Application.Extensions;
using CodeClash.Core.Models.Domain;
using CodeClash.Core.Models.DTOs;
using CodeClash.Persistence.Entities;
using CodeClash.Persistence.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.SignalR;

namespace CodeClash.Application.Services;

public class RoomService(
    RoomsRepository roomsRepository,
    IssuesRepository issuesRepository,
    UsersRepository usersRepository)
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
        // TODO: проверить работает ли это хрень, как задумывалось)
        var roomEntity = await roomsRepository.GetRoomById(roomId);
        if (roomEntity is null)
            return Result.Failure<Room>($"Room with {roomId} id does not exist.");

        var userEntity = await usersRepository.GetUserById(userId);
        if (userEntity is null)
            return Result.Failure<Room>($"User with {userId} id does not exist.");

        if (userEntity.RoomId is not null && userEntity.RoomId != roomId)
            return Result.Failure<Room>("User is already in room.");

        if (userEntity.RoomId is null)
        {
            userEntity.RoomId = roomId;
            await usersRepository.UpdateUser(userEntity);
            roomEntity.ParticipantsCount += 1;
            await roomsRepository.UpdateRoom(roomEntity);
        }

        var room = await GetRoomByEntity(roomEntity);

        return Result.Success(room);
    }

    public async Task<Result<Room?>> QuitRoom(Guid userId, Guid roomId, HubCallerContext hubCallerContext,
        IGroupManager groupManager)
    {
        var userEntity = await usersRepository.GetUserById(userId);
        if (userEntity is null)
            return Result.Failure<Room?>($"User with {userId} id does not exist.");

        var roomEntity = await roomsRepository.GetRoomById(roomId);
        if (roomEntity is null)
            return Result.Failure<Room?>("Room does not exist.");
        if (roomEntity.Id != roomId)
            return Result.Failure<Room?>($"User is not in room with id {roomId}.");

        if (userEntity.IsAdmin)
        {
            userEntity.IsAdmin = false;
            userEntity.ClearUserEntityOverhead();
            await usersRepository.UpdateUser(userEntity);
            await roomsRepository.Delete(roomEntity);
            return Result.Success<Room?>(null);
        }

        userEntity.RoomId = null;
        userEntity.ClearUserEntityOverhead();
        await usersRepository.UpdateUser(userEntity);
        await groupManager.RemoveFromGroupAsync(hubCallerContext.ConnectionId, roomId.ToString());
        roomEntity.ParticipantsCount -= 1;
        await roomsRepository.UpdateRoom(roomEntity);

        var room = await GetRoomByEntity(roomEntity);
        return Result.Success<Room?>(room);
    }

    public async Task<Result<List<RoomDTO>>> GetAllWaitingRoomDtos()
    {
        var roomEntities = await roomsRepository.GetRooms();
        return Result.Success(roomEntities
            .Where(r => r.Status == RoomStatus.WaitingForParticipants)
            .Select(r => r.GetRoomDtoFromRoomEntity()).ToList());
    }

    public async Task<Result<bool>> IsUserAdmin(Guid userId)
    {
        var userEntity = await usersRepository.GetUserById(userId);
        return userEntity is null
            ? Result.Failure<bool>($"User with {userId} does not exist.")
            : Result.Success(userEntity.IsAdmin);
    }

    public async Task<Result<Room>> GetRoomByRoomId(Guid roomId)
    {
        var roomEntity = await roomsRepository.GetRoomById(roomId);
        if (roomEntity is null)
            return Result.Failure<Room>("Room does not exist.");

        var issue = (await issuesRepository.GetIssueById(roomEntity.IssueId))!.GetIssueFromEntity();
        var room = roomEntity.GetRoomFromEntity(issue);

        var participants = (await usersRepository.GetUsersByRoomId(roomId))
            .Select(u => u.GetUserFromEntity())
            .ToList();
        room.SetParticipants(participants);

        return Result.Success(room);
    }

    public async Task<Result<RoomEntity>> GetRoomEntityById(Guid roomId)
    {
        var roomEntity = await roomsRepository.GetRoomById(roomId);
        return roomEntity is null
            ? Result.Failure<RoomEntity>($"Room with {roomId} does not exist.")
            : Result.Success(roomEntity);
    }

    public async Task<List<UserEntity>> GetRoomLeadersByRoomId(Guid roomId)
    {
        return await usersRepository.GetUsersByRoomIdInOrderByKey(roomId, user => user.CompetitionOverhead);
    }

    private async Task<Room> GetRoomByEntity(RoomEntity roomEntity)
    {
        var issue = (await issuesRepository.GetIssueById(roomEntity.IssueId))!.GetIssueFromEntity();
        var room = roomEntity.GetRoomFromEntity(issue);

        var participants = (await usersRepository.GetUsersByRoomId(roomEntity.Id))
            .Select(u => u.GetUserFromEntity())
            .ToList();
        room.SetParticipants(participants);

        return room;
    }
}