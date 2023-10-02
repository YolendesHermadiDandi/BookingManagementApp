using API.Models;

namespace API.Contracts;

//class child hasil inheritance/pewarisan dari class IGeneralrepository
public interface IAccountsRepository : IGeneralRepository<Accounts>
{
}