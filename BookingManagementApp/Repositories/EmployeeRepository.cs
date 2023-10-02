using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly BookingManagementDbContext _context;

        public EmployeeRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employees> GetAll()
        {
            return _context.Set<Employees>().ToList();
        }

        public Employees? GetByGuid(Guid guid)
        {
            return _context.Set<Employees>().Find(guid);
        }

        public Employees? Create(Employees employees)
        {
            try
            {
                _context.Set<Employees>().Add(employees);
                _context.SaveChanges();
                return employees;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(Employees employees)
        {
            try
            {
                _context.Set<Employees>().Update(employees);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Employees employees)
        {
            try
            {
                _context.Set<Employees>().Remove(employees);
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
