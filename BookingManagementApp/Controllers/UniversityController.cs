using API.Contracts;
using API.DTOs.Account;
using API.DTOs.University;
using API.Models;
using API.Repositories;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;

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
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data NOT FOUND"
            });
        }

        //Linq
        var data = result.Select(x => (UniversityDto)x);

        return Ok(new ResponseOkHandler<IEnumerable<UniversityDto>>(data));
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
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data NOT FOUND"
            });
        }
        return Ok(new ResponseOkHandler<UniversityDto>((UniversityDto)result)); //konversi explisit
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
        try
        {
            var result = _universityRepository.Create(createUniversityDto);
            return Ok(new ResponseOkHandler<UniversityDto>((UniversityDto)result));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
             new ResponseErrorHandler
             {
                 Code = StatusCodes.Status500InternalServerError,
                 Status = HttpStatusCode.NotFound.ToString(),
                 Message = "FAIL TO CREATE DATA"
             });
        }
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
        try
        {


            var entity = _universityRepository.GetByGuid(universityDto.Guid);
            if (entity is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "ID NOT FOUND"
                });
            }
            Universities toUpdate = universityDto;
            toUpdate.CreateDate = entity.CreateDate;


            var result = _universityRepository.Update(toUpdate);
            return Ok(new ResponseOkHandler<string>("DATA UPDATED"));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
             new ResponseErrorHandler
             {
                 Code = StatusCodes.Status500InternalServerError,
                 Status = HttpStatusCode.NotFound.ToString(),
                 Message = "FAIL TO UPDATE DATA"
             });
        }
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
        try
        {
            var existingUniversity = _universityRepository.GetByGuid(guid); ;
            if (existingUniversity is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "ID NOT FOUND"
                });
            }

            var result = _universityRepository.Delete(existingUniversity);
            return Ok(new ResponseOkHandler<string>("DATA DELETED"));

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
             new ResponseErrorHandler
             {
                 Code = StatusCodes.Status500InternalServerError,
                 Status = HttpStatusCode.NotFound.ToString(),
                 Message = "FAIL TO DELETE DATA"
             });
        }
    }
}
