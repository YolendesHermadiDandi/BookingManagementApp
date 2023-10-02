using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IEducationyRepository _educationRepository;

        public EducationController(IEducationyRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _educationRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _educationRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Education education)
        {
            var result = _educationRepository.Create(education);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut("{guid}")]
        public IActionResult Update(Guid guid, [FromBody] Education updatedEducation)
        {
            var existingEducation = _educationRepository.GetByGuid(guid); ;
            if (existingEducation is null)
            {
                return NotFound("Id Not Found");
            }

            existingEducation.Major = updatedEducation.Major;
            existingEducation.Degree = updatedEducation.Degree;
            existingEducation.Gpa = updatedEducation.Gpa;
            existingEducation.UniversityGuid = updatedEducation.UniversityGuid;
            existingEducation.ModifiedeDate = updatedEducation.ModifiedeDate;


            var result = _educationRepository.Update(existingEducation);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok(result);
        }

        [HttpDelete("{guid}")]
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
