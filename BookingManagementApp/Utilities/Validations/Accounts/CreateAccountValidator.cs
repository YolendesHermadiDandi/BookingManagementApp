using API.DTOs.Account;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
   
    public class CreateAccountValidator : AbstractValidator<CreateAccountDto>
    {
        public CreateAccountValidator()
        {
            /*
             * Pembuatan role pada password dengan berbagai ketentuan
             * password harus mengandung setidaknya
             * 1 huruf besar, 1 huruf kecil, 1 angka, dan 1 simbol
             * 
             */
            RuleFor(a => a.Password)
                     .NotEmpty()
                     .MinimumLength(8)        
                     .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                     .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                     .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                     .Matches(@"[\!\?\*\.]*$").WithMessage("Your password must contain at least one (!? *.).");

        }
    }
}
