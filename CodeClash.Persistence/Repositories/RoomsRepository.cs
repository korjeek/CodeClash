using CodeClash.Core.Models;
using CodeClash.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence.Repositories;

public class RoomsRepository(ApplicationDbContext dbContext)
{
    public async Task<RoomEntity?> Add(RoomEntity roomEntity, Guid userId)
    {
        var admin = await dbContext.Users.FindAsync(userId);
        if (admin!.IsAdmin)
            return null;
        
        dbContext.Rooms.Attach(roomEntity);
        admin.IsAdmin = true;
        admin.Room = roomEntity;
        
        await dbContext.SaveChangesAsync();
        
        return roomEntity;
    }

    public async Task<RoomEntity?> GetRoomById(Guid roomId)
    {
        return await dbContext.Rooms
            .Include(r => r.Participants)
            .Include(r => r.IssueEntity)
            .FirstOrDefaultAsync(r => r.Id == roomId);
    }
    
    public async Task<RoomEntity?> AddUserToRoom(Guid userId, Guid roomId)
    {
        var room = await GetRoomById(roomId);
        if (room is null || room.Status is RoomStatus.CompetitionInProgress)
            return null;

        var user = await dbContext.Users.FindAsync(userId);
        room.Participants.Add(user!); 
        await dbContext.SaveChangesAsync();
        return room;
    }

    public async Task<RoomEntity?> RemoveUserFromRoom(Guid userId, Guid roomId)
    {
        var user = await dbContext.Users.FindAsync(userId);
        var room = await GetRoomById(roomId);
        if (room is null)
            return null;
        room.Participants.Remove(user!);
        if (user!.IsAdmin)
            user.IsAdmin = false;
        await dbContext.SaveChangesAsync();
        return room;
    }
}