using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    //class child hasil inheritance/pewarisan dari class Generalrepository
    public class AccountRoleRepository : GeneralRepository<AccountRoles>, IAccountRoleRepository
    {
        public AccountRoleRepository(BookingManagementDbContext context) : base(context) { }

        public IEnumerable<Guid> GetRolesGuidByAccountGuid(Guid accountGuid)
        {
            return _context.Set<AccountRoles>().Where(ar => ar.AccountGuid == accountGuid).Select(ar => ar.RoleGuid);
        }
    }
}
