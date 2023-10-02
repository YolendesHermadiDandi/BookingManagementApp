using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    //class child hasil inheritance/pewarisan dari class Generalrepository
    public class RoleRepository : GeneralRepository<Roles>, IRoleRepository
    {
      
        public RoleRepository(BookingManagementDbContext context) : base(context) { }

    }
}
