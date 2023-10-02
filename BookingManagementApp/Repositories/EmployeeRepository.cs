using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    //class child hasil inheritance/pewarisan dari class Generalrepository
    public class EmployeeRepository : GeneralRepository<Employees>, IEmployeeRepository
    {

        public EmployeeRepository(BookingManagementDbContext context) : base(context) { }

    }
}
