using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly BookingManagementDbContext _context;

        public RoomRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Rooms> GetAll()
        {
            return _context.Set<Rooms>().ToList();
        }

        public Rooms? GetByGuid(Guid guid)
        {
            return _context.Set<Rooms>().Find(guid);
        }

        public Rooms? Create(Rooms rooms)
        {
            try
            {
                _context.Set<Rooms>().Add(rooms);
                _context.SaveChanges();
                return rooms;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(Rooms rooms)
        {
            try
            {
                _context.Set<Rooms>().Update(rooms);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Rooms rooms)
        {
            try
            {
                _context.Set<Rooms>().Remove(rooms);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
