using API.Models;

namespace API.Contracts;

public interface IUniversityRepository
{
    IEnumerable<Universities> GetAll();
    Universities? GetByGuid(Guid guid);
    Universities? Create(Universities university);
    bool Update(Universities university);
    bool Delete(Universities university);
}