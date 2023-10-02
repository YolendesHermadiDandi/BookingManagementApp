using API.Contracts;
using API.DTOs.University;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

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

        var data = result.Select(x => (UniversityDto)x);

        return Ok(data);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _universityRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok((UniversityDto)result);
    }

    [HttpPost]
    public IActionResult Create(CreateUniversityDto createUniversityDto)
    {
        var result = _universityRepository.Create(createUniversityDto);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok((UniversityDto)result);
    }


    [HttpPut]
    public IActionResult Update(UniversityDto universityDto)
    {
        var entity = _universityRepository.GetByGuid(universityDto.Guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }
        Universities toUpdate = universityDto;
        toUpdate.CreateDate = entity.CreateDate;


        var result = _universityRepository.Update(toUpdate);
        if (!result)
        {
            return BadRequest("failed to update data");
        }

        return Ok("Data update success");
    }

    [HttpDelete]
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
        return Ok("Data deleted");
    }
}
