using CodeClash.Core.Models;
namespace CodeClash.Persistence.Repositories;

public class RoomsRepository 
{
    public async Task<string> Add(Room room)
    {
        return await new Task<string>(() => "HUI IS CREATED");
    }
    
    public async Task<Room?> GetRoomById(Guid id)
    {
        throw new NotImplementedException();
    }
}