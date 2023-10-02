using API.Models;

namespace API.Contracts
{
    public interface IRoleRepository
    {
        IEnumerable<Roles> GetAll();
        Roles? GetByGuid(Guid guid);
        Roles? Create(Roles roles);
        bool Update(Roles roles);
        bool Delete(Roles roles);
    }
}
