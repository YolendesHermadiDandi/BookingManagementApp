using API.DTOs.Employee;
using API.Models;

namespace API.DTOs.Role
{
    public class CreateRoleDto
    {
        public string Name {  get; set; }


        /* method implicit yang digunaakan untuk create Account Role
        * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
        *
        */
        public static implicit operator Roles(CreateRoleDto createRoleDto)
        {
            return new Roles
            {
                Name = createRoleDto.Name,
                CreateDate = DateTime.Now,
                ModifiedeDate = DateTime.Now,
            };
        }
    }
}
