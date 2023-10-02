using API.Contracts;
using API.DTOs.University;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Controllers;

[ApiController]
//API route
[Route("api/[controller]")]
public class UniversityController : ControllerBase
{
    private readonly IUniversityRepository _universityRepository;
    //Constructor
    public UniversityController(IUniversityRepository universityRepository)
    {
        _universityRepository = universityRepository;
    }

    [HttpGet] //http request method
    //get All data
    public IActionResult GetAll()
    {
        var result = _universityRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }

        //Linq
        var data = result.Select(x => (UniversityDto)x);

        return Ok(data);
    }

    [HttpGet("{guid}")]
    /*
    * method dibawah digunakan untuk mendapatkan data berdasarkan guid
    * 
    * PHARAM :
    * - guid : primary key dari 1 baris data
    */
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _universityRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok((UniversityDto)result); //konversi explisit
    }

    [HttpPost]
    /*
     * Method dibawah digunakan untuk memasukan data dengan menggunakan parameter dari method DTO
     * 
     * PHARAM :
     * - createAccountDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
     */
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
    /*
     * Method dibawah digunakan untuk mengupdate data dengan menggunakan parameter dari method DTO
     * 
     * PHARAM :
     * - universitytDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
     */
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

    [HttpDelete("{guid}")]
    /*
    * Method dibawah digunakan untuk menghapus data dengan menggunakan guid
    * 
    * PHARAM :
    * - guid : primary key dari 1 baris data
    */
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
