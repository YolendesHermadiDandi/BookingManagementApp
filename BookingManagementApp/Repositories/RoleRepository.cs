using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BookingManagementDbContext _context;

        public RoleRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Roles> GetAll()
        {
            return _context.Set<Roles>().ToList();
        }

        public Roles? GetByGuid(Guid guid)
        {
            return _context.Set<Roles>().Find(guid);
        }

        public Roles? Create(Roles roles)
        {
            try
            {
                _context.Set<Roles>().Add(roles);
                _context.SaveChanges();
                return roles;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(Roles roles)
        {
            try
            {
                _context.Set<Roles>().Update(roles);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Roles roles)
        {
            try
            {
                _context.Set<Roles>().Remove(roles);
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
