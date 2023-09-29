using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UniversityController : ControllerBase
{
    private readonly IUniversityRepository _universityRepository;

    public UniversityController(IUniversityRepository universityRepository)
    {
        _universityRepository = universityRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _universityRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }

        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _universityRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(Universities university)
    {
        var result = _universityRepository.Create(university);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok(result);
    }

    [HttpPut("{guid}")]
    public IActionResult Update(Guid guid, [FromBody] Universities updatedUniversity)
    {
        var existingUniversity = _universityRepository.GetByGuid(guid); ;
        if (existingUniversity is null)
        {
            return NotFound("Id Not Found");
        }

        existingUniversity.Name = updatedUniversity.Name;
        existingUniversity.Code = updatedUniversity.Code;
        existingUniversity.ModifiedeDate = updatedUniversity.ModifiedeDate;


        var result = _universityRepository.Update(existingUniversity);
        if (!result)
        {
            return BadRequest("Failed to update data");
        }

        return Ok(result);
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid) 
    {
        var existingUniversity = _universityRepository.GetByGuid(guid); ;
        if (existingUniversity is null)
        {
            return NotFound("Id Not Found");
        }

        var result = _universityRepository.Delete(existingUniversity);
        if (!result)
        {
            return NotFound("Delete failed");
        }
        return Ok(result);
    }
}
