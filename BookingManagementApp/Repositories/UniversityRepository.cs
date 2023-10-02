using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

//class child hasil inheritance/pewarisan dari class Generalrepository
public class UniversityRepository : GeneralRepository<Universities>, IUniversityRepository
{

    public UniversityRepository(BookingManagementDbContext context) : base(context) { }


}