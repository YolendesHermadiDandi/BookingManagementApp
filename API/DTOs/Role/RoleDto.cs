using API.DTOs.University;
using API.Models;

namespace API.DTOs.Role
{
    public class RoleDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }

        /*
         * Method explicit digunakan supaya ketika melakukan konversi data
         * perlu melakukan casting (penjelasan dari tipe data yang akan di konvert)
         * sehingga jika ingin melakukan konversi data perlu melakukan casting
         * supaya memasukan data yang dikonvert itu benar.
         * 
         */
        public static explicit operator RoleDto(Roles roles)
        {
            return new RoleDto
            {
               Guid = roles.Guid,
               Name = roles.Name,
            };
        }

        /*
         * method implicit yang digunaakan untuk create university
         * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
         * 
         */
        public static implicit operator Roles(RoleDto roleDto)
        {
            return new Roles
            {
                Guid = roleDto.Guid,
                Name = roleDto.Name,
                ModifiedeDate = DateTime.Now
            };
        }
    }
}
