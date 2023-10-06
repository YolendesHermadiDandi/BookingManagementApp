using API.DTOs.AccountRole;
using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Booking
{
    public class BookingDto
    {
        //setter getter
        public Guid Guid { get; set; }
        //public string BookedNik {  get; set; }
        //public string BookedBy {  get; set; }
        //public string RoomName {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }
        //public Guid RoomGuid { get; set; }
        //public Guid EmployeeGuid { get; set; }


        /*
        * Method explicit digunakan supaya ketika melakukan konversi data
        * perlu melakukan casting (penjelasan dari tipe data yang akan di konvert)
        * sehingga jika ingin melakukan konversi data perlu melakukan casting
        * supaya memasukan data yang dikonvert itu benar.
        * 
        */
        public static explicit operator BookingDto(Bookings bookings)
        {
            return new BookingDto
            {
                Guid = bookings.Guid,
                StartDate = bookings.StartDate,
                EndDate = bookings.EndDate,
                Status = bookings.Status,
                Remarks = bookings.Remarks,
                //RoomGuid = bookings.RoomGuid,
                //EmployeeGuid = bookings.EmployeeGuid,

            };
        }

        /*
         * method implicit yang digunaakan untuk create
         * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
         * 
         */
        public static implicit operator Bookings(BookingDto bookingDto)
        {
            return new Bookings
            {
                Guid = bookingDto.Guid,
                StartDate = bookingDto.StartDate,
                EndDate = bookingDto.EndDate,
                Status = bookingDto.Status,
                Remarks = bookingDto.Remarks,
                //RoomGuid = bookingDto.RoomGuid,
                //EmployeeGuid = bookingDto.EmployeeGuid,
                ModifiedeDate = DateTime.Now,
            };
        }
    }
}
