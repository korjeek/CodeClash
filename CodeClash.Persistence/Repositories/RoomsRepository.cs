using CodeClash.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence.Repositories;

public class RoomsRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
{
    public async Task Add(RoomEntity room)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        dbContext.Rooms.Add(room);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(RoomEntity room)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        dbContext.Rooms.Remove(room);
        await dbContext.SaveChangesAsync();
    }

    public async Task<RoomEntity?> GetRoomById(Guid roomId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        return await dbContext.Rooms.FindAsync(roomId);
    }
    
    public async Task<List<RoomEntity>> GetRooms()
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        return await dbContext.Rooms.ToListAsync();
    }
    
    public async Task UpdateRoom(RoomEntity roomEntity)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        await dbContext.Rooms
            .Where(r => r.Id == roomEntity.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(r => r.Status, roomEntity.Status)
                .SetProperty(r => r.ParticipantsCount, roomEntity.ParticipantsCount));
    }
}