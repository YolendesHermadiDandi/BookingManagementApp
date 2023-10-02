using API.Contracts;
using API.DTOs.Role;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //API route
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        //Constructor
        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet] //http request method
        //get All data
        public IActionResult GetAll()
        {
            var result = _roleRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            var data = result.Select(x => (RoleDto)x);

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
            var result = _roleRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok((RoleDto)result); //konversi explisit
        }

        [HttpPost]
        /*
         * Method dibawah digunakan untuk memasukan data dengan menggunakan parameter dari method DTO
         * 
         * PHARAM :
         * - createRoleDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
         */
        public IActionResult Create(CreateRoleDto createRoleDto)
        {
            var result = _roleRepository.Create(createRoleDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((RoleDto)result);
        }

        [HttpPut]
        /*
      * Method dibawah digunakan untuk mengupdate data dengan menggunakan parameter dari method DTO
      * 
      * PHARAM :
      * - roleDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
      */
        public IActionResult Update(RoleDto roleDto)
        {
            var existingRole = _roleRepository.GetByGuid(roleDto.Guid);
            if (existingRole is null)
            {
                return NotFound("Id Not Found");
            }
            Roles toUpdate = roleDto;
            toUpdate.CreateDate = existingRole.CreateDate;

            var result = _roleRepository.Update(toUpdate);
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
