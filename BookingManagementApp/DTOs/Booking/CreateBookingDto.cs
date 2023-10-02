using API.DTOs.AccountRole;
using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Booking
{
    public class CreateBookingDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }

        /*
        * method implicit yang digunaakan untuk create Account Role
        * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
        * 
        */
        public static implicit operator Bookings(CreateBookingDto bookingDto)
        {
            return new Bookings
            {
                StartDate = bookingDto.StartDate,
                EndDate = bookingDto.EndDate,
                Status = bookingDto.Status,
                Remarks = bookingDto.Remarks,
                RoomGuid = bookingDto.RoomGuid,
                EmployeeGuid = bookingDto.EmployeeGuid,
                CreateDate = DateTime.Now,
                ModifiedeDate = DateTime.Now,
            };
        }
    }
}
