using API.Models;

namespace API.Contracts;

//class child hasil inheritance/pewarisan dari class IGeneralrepository
public interface IUniversityRepository : IGeneralRepository<Universities>
{
    Universities GetUniversities(string code, string name);

}