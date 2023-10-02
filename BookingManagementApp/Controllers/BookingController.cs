using API.Contracts;
using API.DTOs.Booking;
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

            var data = result.Select(x => (BookingDto)x);

            return Ok((BookingDto)result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _bookingRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok((BookingDto)result);
        }

        [HttpPost]
        public IActionResult Create(CreateBookingDto createBookingDto)
        {
            var result = _bookingRepository.Create(createBookingDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((BookingDto)result);
        }

        [HttpPut]
        public IActionResult Update(BookingDto bookingDto)
        {
            var existingBooikng = _bookingRepository.GetByGuid(bookingDto.Guid); ;
            if (existingBooikng is null)
            {
                return NotFound("Id Not Found");
            }

            Bookings toUpdate = bookingDto;
            toUpdate.CreateDate = existingBooikng.CreateDate;

            var result = _bookingRepository.Update(toUpdate);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok("Data update success");
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
