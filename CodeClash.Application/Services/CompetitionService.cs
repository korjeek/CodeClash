using CodeClash.Persistence.Entities;
using CodeClash.Persistence.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.SignalR;

namespace CodeClash.Application.Services;

public class CompetitionService(RoomsRepository roomsRepository, UsersRepository usersRepository)
{
    public async Task<Result<RoomEntity>> UpdateRoomStatus(Guid roomId, RoomStatus status)
    {
        var roomEntity = await roomsRepository.GetRoomById(roomId);
        if (roomEntity is null)
            return Result.Failure<RoomEntity>("Room does not exist.");
        roomEntity.Status = status;
        await roomsRepository.UpdateRoom(roomEntity);
        return Result.Success(roomEntity);
    }

    public async Task<Result<bool>> GetUserStatus(Guid userId)
    {
        var user = await usersRepository.GetUserById(userId);
        if (user is null)
            return Result.Failure<bool>("User does not exist.");
        return Result.Success(user.IsAdmin);
    }
    
    public async Task<Result<RoomStatus>> GetRoomStatus(Guid roomId)
    {
        var room = await roomsRepository.GetRoomById(roomId);
        if (room is null)
            return Result.Failure<RoomStatus>("Room does not exist.");
        return Result.Success(room.Status);
    }
    
    public async Task SyncTimers(IClientProxy clients, TimeOnly duration, Guid roomId)
    {
        var endTime = DateTime.Now + duration.ToTimeSpan().Duration();

        while (true)
        {
            if (DateTime.Now >= endTime)
            {
                await UpdateRoomStatus(roomId, RoomStatus.WaitingForParticipants);
                await clients.SendAsync("CompetitionEnded"); // Метод, который будет выполняться на фронте
                break;
            }
            
            var leftTime = endTime - DateTime.Now;
            await clients.SendAsync("UpdateTimer", $"{leftTime.Minutes}m {leftTime.Seconds}s"); // Метод, который будет выполняться на фронте
            await Task.Delay(1000); // Синхронизация раз в секунду
        }
    }
}