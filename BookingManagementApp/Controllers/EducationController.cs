using API.Contracts;
using API.DTOs.Account;
using API.DTOs.Educations;
using API.Models;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    //API route
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IEducationyRepository _educationRepository;

        //Constructor
        public EducationController(IEducationyRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        [HttpGet] //http request method
        //get All data
        public IActionResult GetAll()
        {
            var result = _educationRepository.GetAll();
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
            var data = result.Select(x => (EducationDto)x);

            return Ok(new ResponseOkHandler<IEnumerable<EducationDto>>(data));
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
            var result = _educationRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }
            return Ok(new ResponseOkHandler<EducationDto>((EducationDto)result)); //konversi explisit
        }

        [HttpPost]
        /*
        * Method dibawah digunakan untuk memasukan data dengan menggunakan parameter dari method DTO
        * 
        * PHARAM :
        * - createEducationDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
        */
        public IActionResult Create(CreateEducationDto createEducationDto)
        {
            try
            {
                var result = _educationRepository.Create(createEducationDto);
                return Ok(new ResponseOkHandler<EducationDto>((EducationDto)result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 new ResponseErrorHandler
                 {
                     Code = StatusCodes.Status500InternalServerError,
                     Status = HttpStatusCode.NotFound.ToString(),
                     Message = "FAILED TO CREATE DATA"
                 });
            }
        }

        [HttpPut]
        /*
      * Method dibawah digunakan untuk mengupdate data dengan menggunakan parameter dari method DTO
      * 
      * PHARAM :
      * - educationDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
      */
        public IActionResult Update(EducationDto educationDto)
        {
            try
            {
                var existingEducation = _educationRepository.GetByGuid(educationDto.Guid); ;
                if (existingEducation is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data NOT FOUND"
                    });
                }

                Education toUpdate = educationDto;
                toUpdate.CreateDate = existingEducation.CreateDate;


                var result = _educationRepository.Update(toUpdate);

                return Ok(new ResponseOkHandler<string>("DATA UPDATED"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 new ResponseErrorHandler
                 {
                     Code = StatusCodes.Status500InternalServerError,
                     Status = HttpStatusCode.NotFound.ToString(),
                     Message = "FAILED TO CREATE DATA"
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
                var existingEducation = _educationRepository.GetByGuid(guid); ;
                if (existingEducation is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data NOT FOUND"
                    });
                }

                var result = _educationRepository.Delete(existingEducation);
                return Ok(new ResponseOkHandler<string>("DATA DELETED"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 new ResponseErrorHandler
                 {
                     Code = StatusCodes.Status500InternalServerError,
                     Status = HttpStatusCode.NotFound.ToString(),
                     Message = "FAILED TO CREATE DATA"
                 });
            }
        }
    }
}
