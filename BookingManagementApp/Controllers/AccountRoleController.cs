using API.Contracts;
using API.DTOs.AccountRole;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleRepository _accountRoleRepository;

        public AccountRoleController(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountRoleRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            var data = result.Select(x => (AccountRoleDto)x);

            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountRoleRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok((AccountRoleDto)result);
        }

        [HttpPost]
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
