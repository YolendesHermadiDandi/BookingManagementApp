using API.Contracts;
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

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _roomRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Rooms rooms)
        {
            var result = _roomRepository.Create(rooms);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut("{guid}")]
        public IActionResult Update(Guid guid, [FromBody] Rooms updatedRoom)
        {
            var existingRoom = _roomRepository.GetByGuid(guid); ;
            if (existingRoom is null)
            {
                return NotFound("Id Not Found");
            }

            existingRoom.Name = updatedRoom.Name;
            existingRoom.Capacity = updatedRoom.Capacity;
            existingRoom.Floor = updatedRoom.Floor;
            existingRoom.ModifiedeDate = updatedRoom.ModifiedeDate;

            var result = _roomRepository.Update(existingRoom);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok(result);
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
