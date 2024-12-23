using System.Collections.Concurrent;
using CodeClash.Core.Models.Domain;
using CodeClash.Persistence.Entities;
using CodeClash.Persistence.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.SignalR;

namespace CodeClash.Application.Services;

public class CompetitionService(RoomsRepository repository)
{
    // private static readonly ConcurrentDictionary<Guid, Competition> Competitions = new();
    public async Task<Result> UpdateRoomStatus(Guid roomId, RoomStatus status)
    {
        var room = await repository.GetRoomById(roomId);
        if (room is null)
            return Result.Failure("Room does not exist.");
        room.Status = status;
        await repository.UpdateRoom(room);
        return Result.Success();
    }

    public async Task<Result<RoomStatus>> GetRoomStatus(Guid roomId)
    {
        var room = await repository.GetRoomById(roomId);
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
            
            await clients.SendAsync("UpdateTimer", (endTime - DateTime.Now).TotalSeconds); // Метод, который будет выполняться на фронте
            await Task.Delay(1000); // Синхронизация раз в секунду
        }
    }
    
    // public async Task<Result<Competition>> StartCompetition(Guid roomId, TimeOnly durationInSeconds, IClientProxy clients)
    // {
    //     var room = await repository.GetRoomById(roomId);
    //     if (room == null)
    //         return Result.Failure<Competition>("Room does not exist.");
    //     if (room.Status == RoomStatus.CompetitionInProgress)
    //         return Result.Failure<Competition>("Competition is already started.");
    //     
    //     room.Status = RoomStatus.CompetitionInProgress;
    //     
    //     
    //     
    //     // await clients.Caller.SendAsync("ReceiveMessage", "Соревнование уже запущено.");
    //
    //     var competition = new Competition(durationInSeconds, roomId, clients);
    //     Competitions[roomId] = competition;
    //     await competition.StartAsync();
    //
    //     // Удаление соревнования после завершения
    //     _ = Task.Run(async () =>
    //     {
    //         while (competition.IsRunning)
    //             await Task.Delay(1000);
    //
    //         Competitions.TryRemove(roomId, out _);
    //     });
    //
    //     clients.SendAsync();
    // }
    //
    // public async Task StopCompetition(Guid roomId, IHubCallerClients clients)
    // {
    //     
    //     if (Competitions.TryGetValue(roomId, out var competition))
    //     {
    //         await competition.StopManuallyAsync();
    //         Competitions.TryRemove(roomId, out _);
    //     }
    //     else
    //     {
    //         await clients.Caller.SendAsync("ReceiveMessage", "Соревнование не найдено.");
    //     }
    // }
}