using CodeClash.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence.Repositories;

public class RoomsRepository(ApplicationDbContext dbContext)
{
    public async Task<Room?> Add(Room room, string adminEmail)
    {
        var admin = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == adminEmail);
        if (admin is null || admin.IsAdmin)
            return null;
        
        dbContext.Rooms.Attach(room);
        admin.IsAdmin = true;
        admin.Room = room;
        
        await dbContext.SaveChangesAsync();
        
        return room;
    }

    public async Task<Room?> GetRoomById(Guid roomId)
    {
        return await dbContext.Rooms
            .Include(r => r.Participants)
            .Include(r => r.Issue)
            .FirstOrDefaultAsync(r => r.Id == roomId);
    }

    public async Task<Room?> AddUserToRoom(User user, Guid roomId)
    {
        var room = await GetRoomById(roomId);
        if (room is null || room.Status is Room.RoomStatus.CompetitionInProgress)
            return null;
        room.Participants.Add(user); 
        await dbContext.SaveChangesAsync();
        return room;
    }
    
    public async Task<Room?> RemoveUserFromRoom(User user, Guid roomId)
    {
        var room = await GetRoomById(roomId);
        if (room is null)
            return null;
        room.Participants.Remove(user);
        if (user.IsAdmin)
            user.IsAdmin = false;
        await dbContext.SaveChangesAsync();
        return room;
    }
}