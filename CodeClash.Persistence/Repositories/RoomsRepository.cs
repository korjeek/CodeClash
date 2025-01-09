using CodeClash.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence.Repositories;

public class RoomsRepository(ApplicationDbContext dbContext)
{
    public async Task Add(RoomEntity room)
    {
        dbContext.Rooms.Add(room);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(RoomEntity room)
    {
        dbContext.Rooms.Remove(room);
        await dbContext.SaveChangesAsync();
    }

    public async Task<RoomEntity?> GetRoomById(Guid roomId)
    {
        return await dbContext.Rooms.FindAsync(roomId);
    }
    
    public Task<List<RoomEntity>> GetRooms()
    {
        return Task.FromResult(dbContext.Rooms.ToList());
    }
    
    public async Task UpdateRoom(RoomEntity roomEntity)
    {
        await dbContext.Rooms
            .Where(r => r.Id == roomEntity.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(r => r.Status, roomEntity.Status)
                .SetProperty(r => r.ParticipantsCount, roomEntity.ParticipantsCount));
    }
}