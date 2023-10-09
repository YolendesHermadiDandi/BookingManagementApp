using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

//class child hasil inheritance/pewarisan dari class Generalrepository
public class UniversityRepository : GeneralRepository<Universities>, IUniversityRepository
{

    public UniversityRepository(BookingManagementDbContext context) : base(context) { }

    public Universities GetUniversities(string code, string name)
    {
        var entity = _context.Set<Universities>().FirstOrDefault(u => u.Code.ToLower() == code.ToLower() ||
                                                                 u.Name.ToLower() == name.ToLower());
        _context.ChangeTracker.Clear();
        return entity;
    }

}