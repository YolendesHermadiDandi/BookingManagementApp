using API.Contracts;
using API.DTOs.Educations;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                return NotFound("Data Not Found");
            }
            //Linq
            var data = result.Select(x => (EducationDto)x);

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
            var result = _educationRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok((EducationDto)result); //konversi explisit
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
            var result = _educationRepository.Create(createEducationDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((EducationDto)result);
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
            var existingEducation = _educationRepository.GetByGuid(educationDto.Guid); ;
            if (existingEducation is null)
            {
                return NotFound("Id Not Found");
            }

            Education toUpdate = educationDto;
            toUpdate.CreateDate = existingEducation.CreateDate;


            var result = _educationRepository.Update(toUpdate);
            if (!result)
            {
                return BadRequest("Failed to update data");
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
            var existingEducation = _educationRepository.GetByGuid(guid); ;
            if (existingEducation is null)
            {
                return NotFound("Id Not Found");
            }

            var result = _educationRepository.Delete(existingEducation);
            if (!result)
            {
                return NotFound("Delete failed");
            }
            return Ok(result);
        }
    }
}
