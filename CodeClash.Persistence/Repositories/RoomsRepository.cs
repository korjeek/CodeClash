using CodeClash.Persistence.Entities;

namespace CodeClash.Persistence.Repositories;

public class RoomsRepository(ApplicationDbContext dbContext)
{
    public async Task<RoomEntity> Add(RoomEntity room)
    {
        dbContext.Rooms.Add(room);
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
    
    public Task<List<RoomEntity>> GetRooms()
    {
        return Task.FromResult(dbContext.Rooms.ToList());
    }

    public Task<List<UserEntity>> GetRoomUsers(Guid roomId)
    {
        return Task.FromResult(dbContext.Users.Where(u => u.RoomId == roomId).ToList());
    }
}