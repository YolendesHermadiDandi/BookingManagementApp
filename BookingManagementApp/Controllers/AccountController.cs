using API.Contracts;
using API.DTOs.Account;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                return NotFound("Data Not Found");
            }

            //Linq
            var data = result.Select(x => (AccountDto)x);

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
            var result = _accountRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok((AccountDto)result); //konversi explisit
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
            var result = _accountRepository.Create(createAccountDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((AccountDto)result);
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
            var existingAccount = _accountRepository.GetByGuid(accountDto.Guid);
            if (existingAccount is null)
            {
                return NotFound("Id Not Found");
            }

            Accounts toUpdate = accountDto;
            toUpdate.CreateDate = existingAccount.CreateDate;

            var result = _accountRepository.Update(toUpdate);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok("Data Update Success");
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
            var existingAccount = _accountRepository.GetByGuid(guid); ;
            if (existingAccount is null)
            {
                return NotFound("Id Not Found");
            }

            var result = _accountRepository.Delete(existingAccount);
            if (!result)
            {
                return NotFound("Delete failed");
            }
            return Ok(result);
        }

    }
}
