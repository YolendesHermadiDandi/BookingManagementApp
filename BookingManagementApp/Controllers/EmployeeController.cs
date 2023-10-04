using API.Contracts;
using API.DTOs.Account;
using API.DTOs.Employee;
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
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }
            //Linq
            var data = result.Select(x => (EmployeeDto)x);

            return Ok(new ResponseOkHandler<IEnumerable<EmployeeDto>>(data));
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
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }
            return Ok(new ResponseOkHandler<EmployeeDto>((EmployeeDto)result)); //konversi explisit
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
            try
            {


                Employees toCreate = createEmployeeDto;
                toCreate.Nik = GenerateHandler.GenerateNik(_employeeRepository.GetLastNik());
                var result = _employeeRepository.Create(toCreate);

                return Ok(new ResponseOkHandler<EmployeeDto>((EmployeeDto)result));
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
        * - employeeDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
        */
        public IActionResult Update(EmployeeDto employeeDto)
        {
            try
            {
                var existingEmployee = _employeeRepository.GetByGuid(employeeDto.Guid);
                if (existingEmployee is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID NOT FOUND"
                    });
                }

                Employees toUpdate = employeeDto;
                toUpdate.CreateDate = existingEmployee.CreateDate;

                var result = _employeeRepository.Update(toUpdate);
                return Ok(new ResponseOkHandler<string>("DATA UPDATED"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "FAIL TO UPDATE DATA",
                    Error = ex.Message
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
                var existingEmployee = _employeeRepository.GetByGuid(guid); ;
                if (existingEmployee is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID NOT FOUND"
                    });
                }

                var result = _employeeRepository.Delete(existingEmployee);
                return Ok(new ResponseOkHandler<string>("DATA DELETED"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 new ResponseErrorHandler
                 {
                     Code = StatusCodes.Status500InternalServerError,
                     Status = HttpStatusCode.NotFound.ToString(),
                     Message = "FAILED TO DELETE DATA"
                 });
            }
        }
    }
}
