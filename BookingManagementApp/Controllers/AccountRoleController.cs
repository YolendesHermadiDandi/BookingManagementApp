using API.Contracts;
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

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountRoleRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(AccountRoles accountRoles)
        {
            var result = _accountRoleRepository.Create(accountRoles);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut("{guid}")]
        public IActionResult Update(Guid guid, [FromBody] AccountRoles updatedAccountRole)
        {
            var existingAccountRole = _accountRoleRepository.GetByGuid(guid); ;
            if (existingAccountRole is null)
            {
                return NotFound("Id Not Found");
            }

            existingAccountRole.AccountGuid = updatedAccountRole.AccountGuid;
            existingAccountRole.RoleGuid = updatedAccountRole.RoleGuid;
            existingAccountRole.ModifiedeDate = updatedAccountRole.ModifiedeDate;

            var result = _accountRoleRepository.Update(existingAccountRole);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok(result);
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
