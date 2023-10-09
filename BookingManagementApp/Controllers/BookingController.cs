using API.Contracts;
using API.DTOs.Account;
using API.DTOs.Booking;
using API.DTOs.Employee;
using API.DTOs.Room;
using API.Models;
using API.Repositories;
using API.Utilities.Enums;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult GetRoomBookingsToday()
        {
            var bookingsToday = _bookingRepository.GetBookingRoomsToday(); //getbooking today
            var employee = _emloyeeRepository.GetAll(); //get all employee
            var rooms = _roomRepository.GetAll(); // get all room

            if (!(employee.Any() && rooms.Any() && bookingsToday.Any()))
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }
            //linq join
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
            var employee = _emloyeeRepository.GetAll(); //get all employee
            var booking = _bookingRepository.GetAll(); //get all booking
            var room = _roomRepository.GetAll(); //get all room

            if (!(employee.Any() && booking.Any() && room.Any()))
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }

            //linq join
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

        [HttpGet("get-room-avilable-today")]
        public IActionResult GetRoomAvailableToday()
        {
            var bookingToday = _bookingRepository.GetBookingRoomsToday(); //get rooms booking today
            var booking = _bookingRepository.GetAll(); //get all booking
            var rooms = _roomRepository.GetAll(); //get all rooms

            if (!(rooms.Any() && booking.Any() && bookingToday.Any()))
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND",
                });
            }

            //linq left join => untuk mendapatkan room yang tidak digunakan hari ini
            var bookingDetail = (from r in rooms
                                 join b in booking on r.Guid equals b.RoomGuid into joined
                                 from b in joined.DefaultIfEmpty()
                                 where r.Guid != b?.RoomGuid || b.Status == StatusLevel.Completed
                                 select new RoomDto
                                 {
                                     Guid = r?.Guid ?? Guid.Empty,
                                     Name = r?.Name,
                                     Floor = r?.Floor ?? 0,
                                     Capacity = r?.Capacity ?? 0
                                 }).ToList();


            return Ok(new ResponseOkHandler<IEnumerable<RoomDto>>(bookingDetail));

        }

        [HttpGet("get-booking-length")]
        public IActionResult GetBookingLength()
        {
            // list buat tamping data room
            List<BookingLengthDto> ListbookingLengthDtos = new List<BookingLengthDto>();

            var bookings = _bookingRepository.GetBookingRoomsToday(); //get booking room today
            if (!bookings.Any())
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND",
                });
            }

            //looping untuk mengecekan tiap booking
            foreach (var booking in bookings)
            {
                var startDate = booking.StartDate; //tanggal mulai booking
                var endDate = booking.EndDate; //tanggal selesai booking
                var totalDayBooking = 0; //total hari selama booking

                //looping selama start date <= end date
                while (startDate <= endDate)
                {
                    //cek apakah hari sekarang merupakan hari sabtu/minggu
                    if (startDate.DayOfWeek != DayOfWeek.Saturday &&
                        startDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        //jika iya total booking +1
                        totalDayBooking++;

                    }
                    // tambahkan 1 hari pada start date
                    startDate = startDate.AddDays(1);
                }
                //ambil room berdasarkan guid
                var room = _roomRepository.GetByGuid(booking.RoomGuid);
                //masukan panjang bookingnya
                var bookingLengthDto = new BookingLengthDto
                {
                    RoomGuid = room.Guid,
                    RoomName = room.Name,
                    BookingLength = string.Concat(totalDayBooking.ToString(), " Hari"),

                };
                //masukan booking length dto ke dalam list
                ListbookingLengthDtos.Add(bookingLengthDto);
            }
            return Ok(new ResponseOkHandler<IEnumerable<BookingLengthDto>>(ListbookingLengthDtos));

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
