using Microsoft.AspNetCore.SignalR;

namespace CodeClash.Core.Models.Domain;

public class Competition
{
    // private readonly string _roomId;
    // private readonly IHubCallerClients _clients;
    // private int _remainingTime;
    // private Timer _timer;
    //
    // public Competition(int durationInSeconds, Guid roomId, IHubCallerClients clients)
    // {
    //     _roomId = roomId.ToString();
    //     _clients = clients;
    //     _remainingTime = durationInSeconds;
    // }
    //
    // public async Task StartAsync()
    // {
    //     if (IsRunning) return;
    //
    //     IsRunning = true;
    //     await _clients.Group(_roomId).SendAsync("ReceiveMessage", "Соревнование началось!");
    //     
    //     // Инициализация и запуск таймера
    //     _timer = new Timer(async _ => await TimerCallback(), null, 0, 1000);
    // }
    //
    // private async Task TimerCallback()
    // {
    //     if (_remainingTime <= 0)
    //     {
    //         await StopAsync("Время истекло!");
    //         return;
    //     }
    //
    //     await _clients.Group(_roomId).SendAsync("UpdateTimer", _remainingTime);
    //     _remainingTime--;
    // }
    //
    // public async Task StopAsync(string message)
    // {
    //     if (!IsRunning) return;
    //
    //     IsRunning = false;
    //     await _timer.DisposeAsync();
    //     _timer = null;
    //     await _clients.Group(_roomId).SendAsync("ReceiveMessage", message);
    //     await _clients.Group(_roomId).SendAsync("CompetitionEnded");
    // }
    //
    // public async Task StopManuallyAsync()
    // {
    //     await StopAsync("Соревнование завершено принудительно.");
    // }
}