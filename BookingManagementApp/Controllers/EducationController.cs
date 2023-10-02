using API.Contracts;
using API.DTOs.Educations;
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

            var data = result.Select(x => (EducationDto)x);

            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _educationRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok((EducationDto)result);
        }

        [HttpPost]
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
