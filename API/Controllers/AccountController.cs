using API.Contracts;
using API.DTOs.Account;
using API.DTOs.Employee;
using API.Models;
using API.Repositories;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Transactions;

namespace API.Controllers
{
    //API route
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountsRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationyRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmailHandlerRepository _emailHandlerRepository;
        private readonly ITokenHandler _tokenHandler;


        //Constructor
        public AccountController(IAccountsRepository accountRepository,
                                 IEmployeeRepository employeeRepository,
                                 IEducationyRepository educationyRepository,
                                 IUniversityRepository universityRepository,
                                 IEmailHandlerRepository emailHandlerRepository,
                                 ITokenHandler tokenHandler,
                                 IAccountRoleRepository accountRoleRepository,
                                 IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _educationRepository = educationyRepository;
            _universityRepository = universityRepository;
            _emailHandlerRepository = emailHandlerRepository;
            _tokenHandler = tokenHandler;
            _accountRoleRepository = accountRoleRepository;
            _roleRepository = roleRepository;
        }

        [HttpGet] //http request method
        //get All data
        public IActionResult GetAll()
        {
            var result = _accountRepository.GetAll();
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
            var data = result.Select(x => (AccountDto)x);
            return Ok(new ResponseOkHandler<IEnumerable<AccountDto>>(data));
        }


        [HttpPut("ForgotPassword")]
        [AllowAnonymous]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                Random random = new Random();
                var existingEmployee = _employeeRepository.GetEmail(email);
                if (existingEmployee is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "EMAIL NOT FOUND"
                    });
                }

                var existingEmployeeAccount = _accountRepository.GetByGuid(existingEmployee.Guid);

                Accounts toUpdate = existingEmployeeAccount;
                toUpdate.OTP = random.Next(111111, 999999);
                toUpdate.IsUsed = false;
                toUpdate.ExpiredTime = DateTime.Now.AddMinutes(5);

                var result = _accountRepository.Update(toUpdate);


                var employee = _employeeRepository.GetAll();
                var account = _accountRepository.GetAll();

                var forgotPassword = from emp in employee
                                     join acc in account on emp.Guid equals acc.Guid
                                     where emp.Email == email
                                     select new ForgotPasswordAccountDto
                                     {
                                         Otp = acc.OTP,
                                         Message = "OTP hanya berlaku 5 menit"
                                     };
                _emailHandlerRepository.Send("Forgot Password", $"Yout OTP is {toUpdate.OTP}", email);
                return Ok(new ResponseOkHandler<IEnumerable<ForgotPasswordAccountDto>>(forgotPassword, "Success send OTP"));
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

        [HttpPut("ChangePassword")]
        [AllowAnonymous]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            try
            {
                var existingEmployee = _employeeRepository.GetEmail(changePasswordDto.Email);
                if (existingEmployee is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "EMAIL NOT FOUND"
                    });
                }

                var existingEmployeeAccount = _accountRepository.GetByGuid(existingEmployee.Guid);

                Accounts toUpdate = existingEmployeeAccount;
                if (existingEmployeeAccount.ExpiredTime < DateTime.Now)
                {

                    toUpdate.IsUsed = true;
                    var result = _accountRepository.Update(toUpdate);
                    return Ok(new ResponseOkHandler<string>("OTP EXPIRED"));
                }

                if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
                {
                    return Ok(new ResponseOkHandler<string>("PASSWORD NOT MATCH"));
                }

                if (existingEmployeeAccount.OTP != changePasswordDto.Otp || existingEmployeeAccount.OTP == 0)
                {

                    return Ok(new ResponseOkHandler<string>("OTP NOT MATCH"));
                }

                toUpdate.OTP = 0;
                toUpdate.IsUsed = false;
                toUpdate.ExpiredTime = existingEmployeeAccount.ExpiredTime;
                toUpdate.Password = HashHandler.HashPassword(changePasswordDto.NewPassword);

                var update = _accountRepository.Update(toUpdate);

                return Ok(new ResponseOkHandler<string>("CHANGE PASSWORD SUCCESS"));
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

        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterAccoutDto registerAccoutDto)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                try
                {

                    if (registerAccoutDto.Password != registerAccoutDto.ConfirmPassword)
                    {
                        return Ok(new ResponseOkHandler<string>("PASSWORD NOT MATCH"));
                    }

                    Employees toCreateEmployee = registerAccoutDto;
                    toCreateEmployee.Nik = GenerateHandler.GenerateNik(_employeeRepository.GetLastNik());
                    var addEmployee = _employeeRepository.Create(toCreateEmployee);
                    var checkUniversity = _universityRepository.GetUniversities(
                                        registerAccoutDto.UniversityCode, registerAccoutDto.UniversityName);
                    if (checkUniversity is null)
                    {
                        checkUniversity = _universityRepository.Create(registerAccoutDto);

                    }
                    Education toCreateEducation = registerAccoutDto;
                    toCreateEducation.UniversityGuid = checkUniversity.Guid;
                    toCreateEducation.Guid = addEmployee.Guid;
                    var eddEducation = _educationRepository.Create(toCreateEducation);

                    Accounts toCreateAccount = registerAccoutDto;
                    toCreateAccount.Guid = addEmployee.Guid;
                    toCreateAccount.Password = HashHandler.HashPassword(registerAccoutDto.Password);

                    var addAccount = _accountRepository.Create(toCreateAccount);

                    //assign role
                    var accountRole = _accountRoleRepository.Create(new AccountRoles 
                    {
                        AccountGuid = toCreateAccount.Guid,
                        RoleGuid = _roleRepository.GetDefaultRoleGuid() ?? 
                        throw new Exception("Default Role Not Found")
                    });

                   
                    transaction.Complete();
                    return Ok(new ResponseOkHandler<string>("REGISTER SUCCESS"));

                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new ResponseErrorHandler
                        {
                            Code = StatusCodes.Status500InternalServerError,
                            Status = HttpStatusCode.InternalServerError.ToString(),
                            Message = "FAILED TO REGISTER",
                            Error = ex.InnerException?.Message ?? ex.Message
                        });
                }
            }
        }

        [HttpGet("Login")]
        [AllowAnonymous]
        public IActionResult Login(string email, string password)
        {
            try
            {
                var existingEmployee = _employeeRepository.GetEmail(email);
                if (existingEmployee is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "EMAIL SALAH"
                    });
                }

                var existingEmployeeAccount = _accountRepository.GetByGuid(existingEmployee.Guid);

                if (!HashHandler.VerifyPassword(password, existingEmployeeAccount.Password))
                {
                    return Ok(new ResponseOkHandler<string>("PASSWORD SALAH"));
                }

                var claims = new List<Claim>();
                claims.Add(new Claim("Email", existingEmployee.Email));
                claims.Add(new Claim("FullName", string.Concat(
                    existingEmployee.FirstName, " ", existingEmployee.LastName)));

                var getRolesName = from ar in _accountRoleRepository.GetAll()
                                   join r in _roleRepository.GetAll() on ar.RoleGuid equals r.Guid
                                   where ar.AccountGuid == existingEmployee.Guid
                                   select r.Name;

                foreach (var role in getRolesName) 
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var generateToken = _tokenHandler.Generate(claims);




                return Ok(new ResponseOkHandler<object>(new { Token = generateToken }, "login success"));
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





        [HttpGet("{guid}")]
        /*
         * method dibawah digunakan untuk mendapatkan data berdasarkan guid
         * 
         * PHARAM :
         * - guid : primary key dari 1 baris data
         */
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }
            return Ok(new ResponseOkHandler<AccountDto>((AccountDto)result)); //konversi explisit
        }

        [HttpPost]

        /*
         * Method dibawah digunakan untuk memasukan data dengan menggunakan parameter dari method DTO
         * 
         * PHARAM :
         * - createAccountDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
         */
        public IActionResult Create(CreateAccountDto createAccountDto)
        {
            try
            {
                Accounts toCreate = createAccountDto;
                toCreate.Password = HashHandler.HashPassword(createAccountDto.Password);
                var result = _accountRepository.Create(toCreate);
                return Ok(new ResponseOkHandler<AccountDto>((AccountDto)result));
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
        * - accountDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
        */
        public IActionResult Update(AccountDto accountDto)
        {
            try
            {
                var existingAccount = _accountRepository.GetByGuid(accountDto.Guid);
                if (existingAccount is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID NOT FOUND"
                    });
                }

                Accounts toUpdate = accountDto;
                toUpdate.CreateDate = existingAccount.CreateDate;
                toUpdate.Password = HashHandler.HashPassword(accountDto.Password);

                var result = _accountRepository.Update(toUpdate);

                return Ok(new ResponseOkHandler<string>("DATA UPDATED"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed to update data"


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
                var existingAccount = _accountRepository.GetByGuid(guid); ;
                if (existingAccount is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID NOT FOUND"
                    });
                }
                var result = _accountRepository.Delete(existingAccount);
                return Ok(new ResponseOkHandler<string>("DATA DELETED"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "FAILED TO DELETED DATA"


                });
            }
        }

    }
}
