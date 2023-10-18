using API.Contracts;
using API.DTOs.Account;
using API.DTOs.Employee;
using API.Models;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;

namespace API.Controllers
{
    //API route
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationyRepository _educationyRepository;
        private readonly IUniversityRepository _universityRepository;
        //Constructor
        public EmployeeController(IEmployeeRepository employeeRepository, IEducationyRepository educationyRepository, IUniversityRepository universityRepository)
        {
            _employeeRepository = employeeRepository;
            _educationyRepository = educationyRepository;
            _universityRepository = universityRepository;
        }



        [HttpGet("Details")]
        [Authorize(Roles = "manager, admin")]
        public IActionResult GetDetails()
        {
            var employee = _employeeRepository.GetAll();
            var education = _educationyRepository.GetAll();
            var university = _universityRepository.GetAll();

            if (!(employee.Any() && education.Any() && university.Any()))
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }

            var employeeDetails = from emp in employee
                                  join edu in education on emp.Guid equals edu.Guid
                                  join uni in university on edu.UniversityGuid equals uni.Guid
                                  select new EmployeeDetailDto
                                  {
                                      Guid = emp.Guid,
                                      Nik = emp.Nik,
                                      FullName = string.Concat(emp.FirstName, " ", emp.LastName),
                                      BirthDate = emp.BirthDate,
                                      Gender = emp.Gender.ToString(),
                                      HiringDate = emp.HiringDate,
                                      Email = emp.Email,
                                      PhoneNumber = emp.PhoneNumber,
                                      Major = edu.Major,
                                      Degree = edu.Degree,
                                      Gpa = edu.Gpa,
                                      University = uni.Name
                                  };

            return Ok(new ResponseOkHandler<IEnumerable<EmployeeDetailDto>>(employeeDetails));

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

        [HttpPost("insert")]
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

                if (result is null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                 new ResponseErrorHandler
                 {
                     Code = StatusCodes.Status500InternalServerError,
                     Status = HttpStatusCode.NotFound.ToString(),
                     Message = "FAILED TO INSERT DATA"
                 });
                }

                return Ok(new ResponseOkHandler<EmployeeDto>((EmployeeDto)result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 new ResponseErrorHandler
                 {
                     Code = StatusCodes.Status500InternalServerError,
                     Status = HttpStatusCode.NotFound.ToString(),
                     Message = "FAILED TO CREATE DATA",
                     Error = ex.Message
                 });
            }

        }

        [HttpPut("update")]
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
                //Employees toUpdate = new Employees();

                //toUpdate.Guid = existingEmployee.Guid;
                existingEmployee.FirstName = employeeDto.FirstName;
                existingEmployee.LastName = employeeDto.LastName;
                existingEmployee.BirthDate = employeeDto.BirthDate;
                existingEmployee.HiringDate = employeeDto.HiringDate;
                existingEmployee.Gender = employeeDto.Gender;
                existingEmployee.Email = employeeDto.Email;
                existingEmployee.PhoneNumber = employeeDto.PhoneNumber;
                //existingEmployee.Nik = existingEmployee.Nik;
                //existingEmployee.CreateDate = existingEmployee.CreateDate;

                var result = _employeeRepository.Update(existingEmployee);

                if (result is false)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                 new ResponseErrorHandler
                 {
                     Code = StatusCodes.Status500InternalServerError,
                     Status = HttpStatusCode.NotFound.ToString(),
                     Message = "FAILED TO UPDATE DATA"
                 });
                }

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
                if (result is false)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                 new ResponseErrorHandler
                 {
                     Code = StatusCodes.Status500InternalServerError,
                     Status = HttpStatusCode.NotFound.ToString(),
                     Message = "FAILED TO DELETE DATA"
                 });

                }

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
