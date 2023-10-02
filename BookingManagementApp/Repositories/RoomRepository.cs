using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    //class child hasil inheritance/pewarisan dari class Generalrepository
    public class RoomRepository : GeneralRepository<Rooms>, IRoomRepository
    {

        public RoomRepository(BookingManagementDbContext context) : base(context) { }


    }
}
