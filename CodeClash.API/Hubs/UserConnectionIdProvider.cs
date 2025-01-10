using CodeClash.API.Extensions;
using Microsoft.AspNetCore.SignalR;

namespace CodeClash.API.Hubs;

public class UserConnectionIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection) => 
        connection.User.GetUserIdFromAccessToken().ToString();
}