using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class AccountRoleRepository : IAccountRoleRepository
    {
        private readonly BookingManagementDbContext _context;

        public AccountRoleRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AccountRoles> GetAll()
        {
            return _context.Set<AccountRoles>().ToList();
        }

        public AccountRoles? GetByGuid(Guid guid)
        {
            return _context.Set<AccountRoles>().Find(guid);
        }

        public AccountRoles? Create(AccountRoles accountRole)
        {
            try
            {
                _context.Set<AccountRoles>().Add(accountRole);
                _context.SaveChanges();
                return accountRole;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(AccountRoles accountRole)
        {
            try
            {
                _context.Set<AccountRoles>().Update(accountRole);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(AccountRoles accountRole)
        {
            try
            {
                _context.Set<AccountRoles>().Remove(accountRole);
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
