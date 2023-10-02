using API.Contracts;
using API.DTOs.Account;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountsRepository _accountRepository;

        public AccountController(IAccountsRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            var data = result.Select(x => (AccountDto)x);

            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok((AccountDto)result);
        }

        [HttpPost]
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
