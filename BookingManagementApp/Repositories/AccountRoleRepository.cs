using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    //class child hasil inheritance/pewarisan dari class Generalrepository
    public class AccountRoleRepository : GeneralRepository<AccountRoles>, IAccountRoleRepository
    {
        public AccountRoleRepository(BookingManagementDbContext context) : base(context) { }
    }
}
