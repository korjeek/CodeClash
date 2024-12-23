using CodeClash.Persistence.Entities;

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
    
    public async Task<List<RoomEntity>?> GetRooms()
    {
        //TODO: get list of active rooms
        throw new NotImplementedException();
    }
}