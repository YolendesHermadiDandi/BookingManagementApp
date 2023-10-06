using API.Models;

namespace API.Contracts
{
    //class child hasil inheritance/pewarisan dari class IGeneralrepository
    public interface IEmployeeRepository : IGeneralRepository<Employees>
    {
        string? GetLastNik();
        Employees GetEmail(string email);
    }
}
