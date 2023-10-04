using API.Contracts;
using API.DTOs.Account;
using API.DTOs.Role;
using API.DTOs.Room;
using API.Models;
using API.Utilities.Handler;
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
                return NotFound(new ResponseDataNotFoundHandler("Data NOT FOUND"));
            }
            //Linq
            var data = result.Select(x => (RoomDto)x);

            return Ok(new ResponseOkHandler<IEnumerable<RoomDto>>(data));
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
                return NotFound(new ResponseDataNotFoundHandler("Data NOT FOUND"));
            }
            return Ok(new ResponseOkHandler<RoomDto>((RoomDto)result)); //konversi explisit
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
            try
            {


                var result = _roomRepository.Create(createRoomDto);
                return Ok(new ResponseOkHandler<RoomDto>((RoomDto)result));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseInternalServerErrorHandler("FAILED TO CREATE DATA", ex));
            }
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
                return NotFound(new ResponseDataNotFoundHandler("ID NOT FOUND"));
            }

            Rooms toUpdate = createRoomDto;
            toUpdate.CreateDate = existingRoom.CreateDate;

            var result = _roomRepository.Update(toUpdate);
            return Ok(new ResponseOkHandler<string>("DATA UPDATED"));
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
                var existingRoom = _roomRepository.GetByGuid(guid); ;
                if (existingRoom is null)
                {
                    return NotFound(new ResponseDataNotFoundHandler("ID NOT FOUND"));
                }

                var result = _roomRepository.Delete(existingRoom);
                return Ok(new ResponseOkHandler<string>("DATA DELETED"));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseInternalServerErrorHandler("FAILED TO DELETED DATA", ex));
            }
        }
    }
}
