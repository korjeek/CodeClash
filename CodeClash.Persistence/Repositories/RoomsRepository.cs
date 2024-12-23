using CodeClash.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence.Repositories;

public class RoomsRepository(ApplicationDbContext dbContext)
{
    public async Task<RoomEntity> Add(RoomEntity room)
    {
        dbContext.Rooms.Attach(room);
        await dbContext.SaveChangesAsync();
        
        return room;
    }

    public async Task<RoomEntity?> GetRoomById(Guid roomId)
    {
        return await dbContext.Rooms.FindAsync(roomId);
        // if (room is null)
        //     return null;
        // var issue = await dbContext.Issues.FindAsync(room.IssueId);
        // if (issue is null)
        //     return null;
        //
        // return Room.Create(roomId, room.Name, room.Time, issue.GetIssueFromEntity()).Value;
    }
    
    public async Task UpdateRoom(RoomEntity roomEntity)
    {
        await dbContext.Rooms
            .Where(r => r.Id == roomEntity.Id)
            .ExecuteUpdateAsync(s => s.
                SetProperty(r => r.Status, roomEntity.Status));
    }

    
    public async Task<List<RoomEntity>?> GetRooms()
    {
        //TODO: get list of active rooms
        throw new NotImplementedException();
    }
}