using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    //class child hasil inheritance/pewarisan dari class Generalrepository
    public class BookingRepository : GeneralRepository<Bookings>, IBookingRepository
    {
        public BookingRepository(BookingManagementDbContext context) : base(context) { }

    }
}
