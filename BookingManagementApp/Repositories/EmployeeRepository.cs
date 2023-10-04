using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    //class child hasil inheritance/pewarisan dari class Generalrepository
    public class EmployeeRepository : GeneralRepository<Employees>, IEmployeeRepository
    {

        public EmployeeRepository(BookingManagementDbContext context) : base(context) { }
        public string? GetLastNik()
        {

            return _context.Set<Employees>().OrderBy(e => e.Nik).LastOrDefault()?.Nik;

        }
    }
}
