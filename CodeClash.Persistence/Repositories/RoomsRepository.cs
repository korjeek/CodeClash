using CodeClash.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence.Repositories;

public class RoomsRepository(ApplicationDbContext dbContext)
{
    public async Task<Room> Add(Room room)
    {
        await dbContext.AddAsync(room);
        await dbContext.SaveChangesAsync();
        return room;
    }

    public async Task<Room?> GetRoomById(Guid roomId)
    {
        return await dbContext.Rooms
            .AsNoTracking()
            .FirstOrDefaultAsync(room => room.Id == roomId);
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
        await dbContext.SaveChangesAsync();
        return room;
    }
}