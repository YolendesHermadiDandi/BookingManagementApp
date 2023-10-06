using API.Models;

namespace API.Contracts;

//class child hasil inheritance/pewarisan dari class IGeneralrepository
public interface IBookingRepository : IGeneralRepository<Bookings>
{
    IEnumerable<Bookings> GetBookingRoomsToday();
}