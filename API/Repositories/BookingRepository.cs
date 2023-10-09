using API.Contracts;
using API.Data;
using API.DTOs.Room;
using API.Models;

namespace API.Repositories
{
    //class child hasil inheritance/pewarisan dari class Generalrepository
    public class BookingRepository : GeneralRepository<Bookings>, IBookingRepository
    {
        public BookingRepository(BookingManagementDbContext context) : base(context) { }

        public IEnumerable<Bookings> GetBookingRoomsToday()
        {
            //mengecek room yang di booking hari ini
            return _context.Set<Bookings>().Where(b => b.StartDate <= DateTime.Now && b.EndDate >= DateTime.Now);

        }

     
        
    }
}
