using API.Contracts;
using API.DTOs.Account;
using API.DTOs.Booking;
using API.DTOs.Employee;
using API.Models;
using API.Repositories;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    //API route
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _emloyeeRepository;
        private readonly IRoomRepository _roomRepository;
        //Constructor
        public BookingController(IBookingRepository bookingRepository, IEmployeeRepository emloyeeRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _emloyeeRepository = emloyeeRepository;
            _roomRepository = roomRepository;
        }

        [HttpGet("get-all")] //http request method
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



        [HttpGet("get-booking-today")]
        [AllowAnonymous]
        public IActionResult GetRoomBookingsToday()
        {
            var bookingsToday = _bookingRepository.GetBookingRoomsToday();
            var employee = _emloyeeRepository.GetAll();
            var rooms = _roomRepository.GetAll();

            if (!(employee.Any() && rooms.Any() && bookingsToday.Any()))
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }

            var bookingDetail = from bt in bookingsToday
                                join emp in employee on bt.EmployeeGuid equals emp.Guid
                                join r in rooms on bt.RoomGuid equals r.Guid
                                select new GetBookingTodayDto
                                {
                                    Guid = bt.Guid,
                                    RoomName = r.Name,
                                    Status = bt.Status.ToString(),
                                    Floor = r.Floor,
                                    BookedBy = string.Concat(emp.FirstName, " ", emp.LastName),

                                };
            return Ok(new ResponseOkHandler<IEnumerable<GetBookingTodayDto>>(bookingDetail));
        }

        [HttpGet("get-booking-detail")]
        public IActionResult getBookingDetail()
        {
            var employee = _emloyeeRepository.GetAll();
            var booking = _bookingRepository.GetAll();
            var room = _roomRepository.GetAll();

            if (!(employee.Any() && booking.Any() && room.Any()))
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }

            var bookingDetail = from b in booking
                                join emp in employee on b.EmployeeGuid equals emp.Guid
                                join r in room on b.RoomGuid equals r.Guid
                                select new DetailBookingDto
                                {
                                    Guid = b.Guid,
                                    BookedNik = emp.Nik,
                                    BookedBy = string.Concat(emp.FirstName, " ", emp.LastName),
                                    RoomName = r.Name,
                                    StartDate = b.StartDate,
                                    EndDate = b.EndDate,
                                    Status = b.Status.ToString(),
                                    Remarks = b.Remarks

                                };
            return Ok(new ResponseOkHandler<IEnumerable<DetailBookingDto>>(bookingDetail));
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
            var employee = _emloyeeRepository.GetByGuid(result.EmployeeGuid);
            var room = _roomRepository.GetByGuid(result.RoomGuid);
            if (result is null && employee is null && room is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }

            var getDetailById = new DetailBookingDto
            {
                Guid = result.Guid,
                BookedNik = employee.Nik,
                BookedBy = string.Concat(employee.FirstName, " ", employee.LastName),
                RoomName = room.Name,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                Status = result.Status.ToString(),
                Remarks = result.Remarks
            };

            

            return Ok(new ResponseOkHandler<DetailBookingDto>(getDetailById)); //konversi explisit
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
