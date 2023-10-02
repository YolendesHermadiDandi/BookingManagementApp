using API.Models;

namespace API.Contracts
{
    public interface IRoomRepository
    {
        IEnumerable<Rooms> GetAll();
        Rooms? GetByGuid(Guid guid);
        Rooms? Create(Rooms rooms);
        bool Update(Rooms rooms);
        bool Delete(Rooms rooms);
    }
}
