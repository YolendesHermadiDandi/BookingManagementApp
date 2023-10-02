using API.Contracts;
using API.DTOs.Role;
using API.DTOs.Room;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //API route
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        //Constructor
        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet] //http request method
        //get All data
        public IActionResult GetAll()
        {
            var result = _roomRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }
            //Linq
            var data = result.Select(x => (RoomDto)x);

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
            var result = _roomRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok((RoomDto)result); //konversi explisit
        }

        [HttpPost]
        /*
         * Method dibawah digunakan untuk memasukan data dengan menggunakan parameter dari method DTO
         * 
         * PHARAM :
         * - createRoomDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
         */
        public IActionResult Create(CreateRoomDto createRoomDto)
        {
            var result = _roomRepository.Create(createRoomDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((RoomDto)result);
        }

        [HttpPut]
        /*
        * Method dibawah digunakan untuk mengupdate data dengan menggunakan parameter dari method DTO
        * 
        * PHARAM :
        * - roomDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
        */
        public IActionResult Update(RoomDto createRoomDto)
        {
            var existingRoom = _roomRepository.GetByGuid(createRoomDto.Guid);
            if (existingRoom is null)
            {
                return NotFound("Id Not Found");
            }

            Rooms toUpdate = createRoomDto;
            toUpdate.CreateDate = existingRoom.CreateDate;

            var result = _roomRepository.Update(toUpdate);
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
            var existingRoom = _roomRepository.GetByGuid(guid); ;
            if (existingRoom is null)
            {
                return NotFound("Id Not Found");
            }

            var result = _roomRepository.Delete(existingRoom);
            if (!result)
            {
                return NotFound("Delete failed");
            }
            return Ok(result);
        }
    }
}
