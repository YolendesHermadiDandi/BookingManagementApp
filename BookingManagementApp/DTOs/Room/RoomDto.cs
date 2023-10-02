using API.DTOs.University;
using API.Models;

namespace API.DTOs.Room
{
    public class RoomDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }

        /*
         * Method explicit digunakan supaya ketika melakukan konversi data
         * perlu melakukan casting (penjelasan dari tipe data yang akan di konvert)
         * sehingga jika ingin melakukan konversi data perlu melakukan casting
         * supaya memasukan data yang dikonvert itu benar.
         * 
         */
        public static explicit operator RoomDto(Rooms rooms)
        {
            return new RoomDto
            {
                Guid = rooms.Guid,
                Name = rooms.Name,
                Floor = rooms.Floor,
                Capacity = rooms.Capacity,
            };
        }

        /*
         * method implicit yang digunaakan untuk create university
         * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
         * 
         */
        public static implicit operator Rooms(RoomDto roomDto)
        {
            return new Rooms
            {
                Guid = roomDto.Guid,
                Name = roomDto.Name,
                Floor = roomDto.Floor,
                Capacity = roomDto.Capacity,
                ModifiedeDate = DateTime.Now
            };
        }
    }
}
