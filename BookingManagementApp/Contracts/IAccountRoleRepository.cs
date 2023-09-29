using API.Models;

namespace API.Contracts;

public interface IAccountRoleRepository
{
    IEnumerable<AccountRoles> GetAll();
    AccountRoles? GetByGuid(Guid guid);
    AccountRoles? Create(AccountRoles accountRole);
    bool Update(AccountRoles accountRoles);
    bool Delete(AccountRoles accountRoles);
}