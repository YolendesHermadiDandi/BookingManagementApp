using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    //class child hasil inheritance/pewarisan dari class Generalrepository
    public class AccountRepository : GeneralRepository<Accounts> ,IAccountsRepository
    {
       public AccountRepository(BookingManagementDbContext context) : base(context) { }
    }
}
