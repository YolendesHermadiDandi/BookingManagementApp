using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class UniversityRepository : GeneralRepository<Universities>, IUniversityRepository
{
    public UniversityRepository(BookingManagementDbContext context) : base(context) { }


}