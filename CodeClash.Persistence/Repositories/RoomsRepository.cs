using CodeClash.Core.Models;

namespace CodeClash.Persistence.Repositories;

public class RoomsRepository 
    // нужно ли сделать дженерик с типом данных, которые мы хотим достать из БД? Код похож на код UserRepository
{
    public async Task Add(Room room)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Room> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}