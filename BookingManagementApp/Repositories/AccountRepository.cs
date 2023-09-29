using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class AccountRepository : IAccountsRepository
    {
        private readonly BookingManagementDbContext _context;

        public AccountRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Accounts> GetAll()
        {
            return _context.Set<Accounts>().ToList();
        }

        public Accounts? GetByGuid(Guid guid)
        {
            return _context.Set<Accounts>().Find(guid);
        }

        public Accounts? Create(Accounts accounts)
        {
            try
            {
                _context.Set<Accounts>().Add(accounts);
                _context.SaveChanges();
                return accounts;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(Accounts accounts)
        {
            try
            {
                _context.Set<Accounts>().Update(accounts);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Accounts accounts)
        {
            try
            {
                _context.Set<Accounts>().Remove(accounts);
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
