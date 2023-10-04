using API.Contracts;
using API.DTOs.Account;
using API.DTOs.Employee;
using API.Models;
using API.Repositories;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    //API route
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountsRepository _accountRepository;

        //Constructor
        public AccountController(IAccountsRepository accountRepository)
        {
            _accountRepository = accountRepository;
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
