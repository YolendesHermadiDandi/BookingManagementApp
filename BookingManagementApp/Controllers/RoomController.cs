using API.Contracts;
using API.DTOs.Role;
using API.DTOs.Room;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roomRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            var data = result.Select(x => (RoomDto)x);

            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _roomRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok((RoomDto)result);
        }

        [HttpPost]
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
