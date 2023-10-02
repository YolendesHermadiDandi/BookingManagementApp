using API.Contracts;
using API.DTOs.Employee;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //API route
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        //Constructor
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet] //http request method
        //get All data
        public IActionResult GetAll()
        {
            var result = _employeeRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }
            //Linq
            var data = result.Select(x => (EmployeeDto)x);

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
            var result = _employeeRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok((EmployeeDto)result); //konversi explisit
        }

        [HttpPost]
        /*
        * Method dibawah digunakan untuk memasukan data dengan menggunakan parameter dari method DTO
        * 
        * PHARAM :
        * - createEmployeeDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
        */
        public IActionResult Create(CreateEmployeeDto createEmployeeDto)
        {
            var result = _employeeRepository.Create(createEmployeeDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((EmployeeDto)result);
        }

        [HttpPut]
        /*
        * Method dibawah digunakan untuk mengupdate data dengan menggunakan parameter dari method DTO
        * 
        * PHARAM :
        * - employeeDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
        */
        public IActionResult Update(EmployeeDto employeeDto)
        {
            var existingEmployee = _employeeRepository.GetByGuid(employeeDto.Guid);
            if (existingEmployee is null)
            {
                return NotFound("Id Not Found");
            }

            Employees toUpdate = employeeDto;
            toUpdate.CreateDate = existingEmployee.CreateDate;

            var result = _employeeRepository.Update(toUpdate);
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
