using API.DTOs.AccountRole;
using API.Models;

namespace API.DTOs.Account
{
    public class CreateAccountDto
    {
        //setter getter

        public Guid Guid { get; set; }
        public string Password { get; set; }
        public int Otp { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }

        /*
         * method implicit yang digunaakan untuk create Account Role
         * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
         * 
         */
        public static implicit operator Accounts(CreateAccountDto createAccountDto)
        {
            return new Accounts
            {
                Guid = createAccountDto.Guid,
                Password = createAccountDto.Password,
                OTP = createAccountDto.Otp,
                IsUsed = createAccountDto.IsUsed,
                ExpiredTime = createAccountDto.ExpiredTime,
                CreateDate = DateTime.Now,
                ModifiedeDate = DateTime.Now,
            };
        }
    }
}
