using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roleRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _roleRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Roles role)
        {
            var result = _roleRepository.Create(role);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut("{guid}")]
        public IActionResult Update(Guid guid, [FromBody] Roles updatedRole)
        {
            var existingRole = _roleRepository.GetByGuid(guid); ;
            if (existingRole is null)
            {
                return NotFound("Id Not Found");
            }

            existingRole.Name = updatedRole.Name;
            existingRole.ModifiedeDate = updatedRole.ModifiedeDate;

            var result = _roleRepository.Update(existingRole);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok(result);
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var existingRole = _roleRepository.GetByGuid(guid); ;
            if (existingRole is null)
            {
                return NotFound("Id Not Found");
            }

            var result = _roleRepository.Delete(existingRole);
            if (!result)
            {
                return NotFound("Delete failed");
            }
            return Ok(result);
        }
    }
}
