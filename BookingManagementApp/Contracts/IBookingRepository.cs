using API.Models;

namespace API.Contracts;

public interface IBookingRepository
{
    IEnumerable<Bookings> GetAll();
    Bookings? GetByGuid(Guid guid);
    Bookings? Create(Bookings bookings);
    bool Update(Bookings bookings);
    bool Delete(Bookings bookings);
}