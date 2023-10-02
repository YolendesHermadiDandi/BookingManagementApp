using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _employeeRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _employeeRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Employees employees)
        {
            var result = _employeeRepository.Create(employees);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut("{guid}")]
        public IActionResult Update(Guid guid, [FromBody] Employees updatedEmployee)
        {
            var existingEmployee = _employeeRepository.GetByGuid(guid); ;
            if (existingEmployee is null)
            {
                return NotFound("Id Not Found");
            }

            existingEmployee.Nik = updatedEmployee.Nik;
            existingEmployee.FirstName = updatedEmployee.FirstName;
            existingEmployee.LastName = updatedEmployee.LastName;
            existingEmployee.BirthDate = updatedEmployee.BirthDate;
            existingEmployee.Gender = updatedEmployee.Gender;
            existingEmployee.HiringDate = updatedEmployee.HiringDate;
            existingEmployee.Email = updatedEmployee.Email;
            existingEmployee.PhoneNumber = updatedEmployee.PhoneNumber;
            existingEmployee.ModifiedeDate = updatedEmployee.ModifiedeDate;


            var result = _employeeRepository.Update(existingEmployee);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok(result);
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var existingEmployee = _employeeRepository.GetByGuid(guid); ;
            if (existingEmployee is null)
            {
                return NotFound("Id Not Found");
            }

            var result = _employeeRepository.Delete(existingEmployee);
            if (!result)
            {
                return NotFound("Delete failed");
            }
            return Ok(result);
        }
    }
}
