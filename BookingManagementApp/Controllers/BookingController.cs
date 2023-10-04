using API.Contracts;
using API.DTOs.Account;
using API.DTOs.Booking;
using API.Models;
using API.Repositories;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    //API route
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        //Constructor
        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet] //http request method
        //get All data
        public IActionResult GetAll()
        {
            var result = _bookingRepository.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }

            var data = result.Select(x => (BookingDto)x);

            return Ok(new ResponseOkHandler<IEnumerable<BookingDto>>(data));
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
            var result = _bookingRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }
            return Ok(new ResponseOkHandler<BookingDto>((BookingDto)result)); //konversi explisit
        }

        [HttpPost]
        /*
        * Method dibawah digunakan untuk memasukan data dengan menggunakan parameter dari method DTO
        * 
        * PHARAM :
        * - createBookingDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
        */
        public IActionResult Create(CreateBookingDto createBookingDto)
        {
            try
            {
                var result = _bookingRepository.Create(createBookingDto);
                return Ok(new ResponseOkHandler<BookingDto>((BookingDto)result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 new ResponseErrorHandler
                 {
                     Code = StatusCodes.Status500InternalServerError,
                     Status = HttpStatusCode.NotFound.ToString(),
                     Message = "FAILED TO CREATE DATA"
                 });
            }
        }

        [HttpPut]
        /*
       * Method dibawah digunakan untuk mengupdate data dengan menggunakan parameter dari method DTO
       * 
       * PHARAM :
       * - bookingDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
       */
        public IActionResult Update(BookingDto bookingDto)
        {
            try
            {

                var existingBooikng = _bookingRepository.GetByGuid(bookingDto.Guid); ;
                if (existingBooikng is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data NOT FOUND"
                    });
                }

                Bookings toUpdate = bookingDto;
                toUpdate.CreateDate = existingBooikng.CreateDate;

                var result = _bookingRepository.Update(toUpdate);
                return Ok(new ResponseOkHandler<string>("DATA UPDATED"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 new ResponseErrorHandler
                 {
                     Code = StatusCodes.Status500InternalServerError,
                     Status = HttpStatusCode.NotFound.ToString(),
                     Message = "FAILED TO UPDATE DATA"
                 });
            }
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
            try
            {
                var existingBooking = _bookingRepository.GetByGuid(guid); ;
                if (existingBooking is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID NOT FOUND"
                    });
                }

                var result = _bookingRepository.Delete(existingBooking);
                return Ok(new ResponseOkHandler<string>("DATA DELETED"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 new ResponseErrorHandler
                 {
                     Code = StatusCodes.Status500InternalServerError,
                     Status = HttpStatusCode.NotFound.ToString(),
                     Message = "FAILED TO DELETE DATA"
                 });
            }
        }
    }
}
