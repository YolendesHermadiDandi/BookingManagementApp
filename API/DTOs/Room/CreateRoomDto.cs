using API.DTOs.University;
using API.Models;

namespace API.DTOs.Room
{
    public class CreateRoomDto
    {
        //setter getter
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }


        /*
         * method implicit yang digunaakan untuk create university
         * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
         * 
         */
        public static implicit operator Rooms(CreateRoomDto roomDto)
        {
            return new Rooms
            {
                Name = roomDto.Name,
                Floor = roomDto.Floor,
                Capacity = roomDto.Capacity,
                CreateDate = DateTime.Now,
                ModifiedeDate = DateTime.Now,
            };
        }
    }
}
