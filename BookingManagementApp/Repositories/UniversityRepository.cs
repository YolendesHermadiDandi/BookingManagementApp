using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class UniversityRepository : IUniversityRepository
{
    private readonly BookingManagementDbContext _context;

    public UniversityRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Universities> GetAll()
    {
        return _context.Set<Universities>().ToList();
    }

    public Universities? GetByGuid(Guid guid)
    {
        return _context.Set<Universities>().Find(guid);
    }

    public Universities? Create(Universities university)
    {
        try
        {
            _context.Set<Universities>().Add(university);
            _context.SaveChanges();
            return university;
        }
        catch
        {
            return null;
        }
    }

    public bool Update(Universities university)
    {
        try
        {
            _context.Set<Universities>().Update(university);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Delete(Universities university)
    {
        try
        {
            _context.Set<Universities>().Remove(university);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}