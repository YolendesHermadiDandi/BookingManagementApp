using API.DTOs.University;
using API.Models;

namespace API.DTOs.AccountRole
{
    public class CreateAccountRoleDto
    {
        //setter getter
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }


        /*
         * method implicit yang digunaakan untuk create Account Role
         * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
         * 
         */
        public static implicit operator AccountRoles(CreateAccountRoleDto createAccountRoleDto)
        {
            return new AccountRoles
            {
               AccountGuid = createAccountRoleDto.AccountGuid,
               RoleGuid = createAccountRoleDto.RoleGuid,
               CreateDate = DateTime.Now,
               ModifiedeDate = DateTime.Now,
            };
        }
    }
}
