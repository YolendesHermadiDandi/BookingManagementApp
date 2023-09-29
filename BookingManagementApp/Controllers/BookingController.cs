using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _bookingRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _bookingRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Bookings booking)
        {
            var result = _bookingRepository.Create(booking);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut("{guid}")]
        public IActionResult Update(Guid guid, [FromBody] Bookings updateBooking)
        {
            var existingBooikng = _bookingRepository.GetByGuid(guid); ;
            if (existingBooikng is null)
            {
                return NotFound("Id Not Found");
            }

            existingBooikng.StartDate = updateBooking.StartDate;
            existingBooikng.EndDate = updateBooking.EndDate;
            existingBooikng.Status = updateBooking.Status;
            existingBooikng.Remarks = updateBooking.Remarks;
            existingBooikng.RoomGuid = updateBooking.RoomGuid;
            existingBooikng.EmployeeGuid = updateBooking.EmployeeGuid;
            existingBooikng.ModifiedeDate = updateBooking.ModifiedeDate;


            var result = _bookingRepository.Update(existingBooikng);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok(result);
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var existingBooking = _bookingRepository.GetByGuid(guid); ;
            if (existingBooking is null)
            {
                return NotFound("Id Not Found");
            }

            var result = _bookingRepository.Delete(existingBooking);
            if (!result)
            {
                return NotFound("Delete failed");
            }
            return Ok(result);
        }
    }
}
