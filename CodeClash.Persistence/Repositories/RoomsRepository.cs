using CodeClash.Core.Extensions;
using CodeClash.Core.Models;
using CodeClash.Core.Models.Enums;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence.Repositories;

public class RoomsRepository(ApplicationDbContext dbContext)
{
    public async Task<Room> Add(Room room)
    {
        var roomEntity = room.GetRoomEntity();
        dbContext.Rooms.Attach(roomEntity);
        await dbContext.SaveChangesAsync();
        
        return room;
    }

    public async Task<Room?> GetRoomById(Guid roomId)
    {
        var room = await dbContext.Rooms.FindAsync(roomId);
        if (room is null)
            return null;
        var issue = await dbContext.Issues.FindAsync(room.IssueId);
        if (issue is null)
            return null;

        return Room.Create(roomId, room.Name, room.Time, issue.GetIssueFromEntity()).Value;
    }
    
    public async Task<List<Room>?> GetRooms()
    {
        //TODO: get list of active rooms
        throw new NotImplementedException();
    }
}