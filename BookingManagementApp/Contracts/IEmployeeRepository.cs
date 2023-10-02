using API.Models;

namespace API.Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employees> GetAll();
        Employees? GetByGuid(Guid guid);
        Employees? Create(Employees employees);
        bool Update(Employees employees);
        bool Delete(Employees employees);
    }
}
