using API.Contracts;
using API.DTOs.AccountRole;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //API route
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleRepository _accountRoleRepository;

        //Constructor
        public AccountRoleController(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        [HttpGet]//http request method
        //get All data
        public IActionResult GetAll()
        {
            var result = _accountRoleRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            //Linq
            var data = result.Select(x => (AccountRoleDto)x);

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
            var result = _accountRoleRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok((AccountRoleDto)result); //konversi explisit
        }

        [HttpPost]
        /*
        * Method dibawah digunakan untuk memasukan data dengan menggunakan parameter dari method DTO
        * 
        * PHARAM :
        * - createAccountRoleDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
        */
        public IActionResult Create(CreateAccountRoleDto createAccountRoleDto)
        {
            var result = _accountRoleRepository.Create(createAccountRoleDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((AccountRoleDto)result);
        }

        [HttpPut]
        /*
        * Method dibawah digunakan untuk mengupdate data dengan menggunakan parameter dari method DTO
        * 
        * PHARAM :
        * - accountRoleDto : kumpulan parameter/method yang sudah ditentukan dari class/objek account
        */
        public IActionResult Update(AccountRoleDto accountRoleDto)
        {
            var existingAccountRole = _accountRoleRepository.GetByGuid(accountRoleDto.Guid); ;
            if (existingAccountRole is null)
            {
                return NotFound("Id Not Found");
            }

            AccountRoles toUpdate = accountRoleDto;
            toUpdate.CreateDate = existingAccountRole.CreateDate;

            var result = _accountRoleRepository.Update(toUpdate);
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
            var existingAccount = _accountRoleRepository.GetByGuid(guid); ;
            if (existingAccount is null)
            {
                return NotFound("Id Not Found");
            }

            var result = _accountRoleRepository.Delete(existingAccount);
            if (!result)
            {
                return NotFound("Delete failed");
            }
            return Ok(result);
        }
    }
}
