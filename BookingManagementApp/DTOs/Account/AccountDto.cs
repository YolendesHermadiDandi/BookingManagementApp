using API.DTOs.AccountRole;
using API.Models;

namespace API.DTOs.Account
{
    public class AccountDto
    {
        //setter getter
        public Guid Guid { get; set; }
        public string Password { get; set; }
        public int Otp { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }

        /*
         * Method explicit digunakan supaya ketika melakukan konversi data
         * perlu melakukan casting (penjelasan dari tipe data yang akan di konvert)
         * sehingga jika ingin melakukan konversi data perlu melakukan casting
         * supaya memasukan data yang dikonvert itu benar.
         * 
         */
        public static explicit operator AccountDto(Accounts accounts)
        {
            return new AccountDto
            {
                Guid = accounts.Guid,
                Password = accounts.Password,
                Otp = accounts.OTP,
                IsUsed = accounts.IsUsed,
                ExpiredTime = accounts.ExpiredTime,

            };
        }

        /*
         * method implicit yang digunaakan untuk create
         * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
         * 
         */
        public static implicit operator Accounts(AccountDto accountDto)
        {
            return new Accounts
            {
                Guid = accountDto.Guid,
                Password = accountDto.Password,
                OTP = accountDto.Otp,
                IsUsed = accountDto.IsUsed,
                ExpiredTime = accountDto.ExpiredTime,
                ModifiedeDate = DateTime.Now
            };
        }

    }
}
