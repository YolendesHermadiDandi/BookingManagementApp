using API.DTOs.University;
using API.Models;

namespace API.DTOs.AccountRole
{
    public class AccountRoleDto
    {
        //setter getter
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        /*
         * Method explicit digunakan supaya ketika melakukan konversi data
         * perlu melakukan casting (penjelasan dari tipe data yang akan di konvert)
         * sehingga jika ingin melakukan konversi data perlu melakukan casting
         * supaya memasukan data yang dikonvert itu benar.
         * 
         */
        public static explicit operator AccountRoleDto(AccountRoles accountRoles)
        {
            return new AccountRoleDto
            {
                Guid = accountRoles.Guid,
                AccountGuid = accountRoles.AccountGuid,
                RoleGuid = accountRoles.RoleGuid,

            };
        }

        /*
         * method implicit yang digunaakan untuk create
         * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
         * 
         */
        public static implicit operator AccountRoles(AccountRoleDto accountRoleDto)
        {
            return new AccountRoles
            {
                Guid = accountRoleDto.Guid,
                AccountGuid = accountRoleDto.AccountGuid,
                RoleGuid = accountRoleDto.RoleGuid,
                ModifiedeDate = DateTime.Now,
            };
        }


    }
}
