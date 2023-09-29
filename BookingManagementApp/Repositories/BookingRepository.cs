using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingManagementDbContext _context;

        public BookingRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Bookings> GetAll()
        {
            return _context.Set<Bookings>().ToList();
        }

        public Bookings? GetByGuid(Guid guid)
        {
            return _context.Set<Bookings>().Find(guid);
        }

        public Bookings? Create(Bookings booking)
        {
            try
            {
                _context.Set<Bookings>().Add(booking);
                _context.SaveChanges();
                return booking;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(Bookings booking)
        {
            try
            {
                _context.Set<Bookings>().Update(booking);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Bookings bookings)
        {
            try
            {
                _context.Set<Bookings>().Remove(bookings);
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
