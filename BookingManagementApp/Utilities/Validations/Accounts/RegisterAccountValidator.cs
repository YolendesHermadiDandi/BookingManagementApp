using API.DTOs.Account;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class RegisterAccountValidator : AbstractValidator<RegisterAccoutDto>
    {
        public RegisterAccountValidator() 
        {

            /*validator untuk first name dengan ketentuan sbb:
             * 1. tidak boleh kosong
             * 2. panjang maksimum 100
             * 3. harus mengandung huruf
             */
            RuleFor(e => e.FirstName)
                .NotEmpty()
                .MaximumLength(100)
                .Matches(@"^[A-Za-z\s]*$").WithMessage("'{PropertyName}' should only contain letters.");

            /*validator untuk last name dengan ketentuan sbb:
            * 1. tidak boleh kosong
            * 2. panjang maksimum 100
            * 3. harus mengandung huruf
            */
            RuleFor(e => e.LastName)
                .MaximumLength(100)
                .Matches(@"^[A-Za-z\s]*$").WithMessage("'{PropertyName}' should only contain letters.");

            /*validator untuk birth date dengan ketentuan sbb:
            * 1. tidak boleh kosong
            * 2. umur untuk employee harus >= 18 tahun
            */
            RuleFor(e => e.BirthDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18));

            /*validator untuk gender dengan ketentuan sbb:
            * 1. tidak boleh kosong
            * 2. dimana gender merupakan enum 0/1 (wanita/pria)
            */
            RuleFor(e => e.Gender)
                .NotNull()
                .IsInEnum();

            //validator untuk hiring date dengan ketentuan hiring date tidak boleh kosong
            RuleFor(e => e.HiringDate)
                .NotEmpty();

            /*validator untuk email dengan ketentuan sbb:
            * 1. tidak boleh kosong
            * 2. harus mengandung @
            * 3. panjang maksimal 100
            */
            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);
            /*validator untuk first name dengan ketentuan sbb:
            * 1. tidak boleh kosong
            * 2. panjang minimal 10
            * 3. panjang maksimal 20
            */
            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                .Length(10, 20);

            //validasi major tidak boleh kosong dan panjang maksimum 100
            RuleFor(e => e.Major)
                .NotEmpty()
                .MaximumLength(100);
            //validasi major tidak boleh kosong dan panjang maksimum 100
            RuleFor(e => e.Degree)
                .NotEmpty()
                .MaximumLength(100);
            //validasi major tidak boleh kosong, <= 4 dan > 0
            RuleFor(e => e.Gpa)
                .NotNull()
                .LessThanOrEqualTo(4)
                .GreaterThan(0);

            /*validator untuk code dengan ketentuan sbb:
             * 1. tidak boleh kosong
             * 2. panjang maksimum 50
             */
            RuleFor(u => u.UniversityCode)
                .NotEmpty()
                .MaximumLength(50);

            /*validator untuk name dengan ketentuan sbb:
            * 1. tidak boleh kosong
            * 2. panjang maksimum 100
            * 3. harus mengandung huruf
            */
            RuleFor(r => r.UniversityName)
               .NotEmpty()
               .MaximumLength(100)
               .Matches(@"^[A-Za-z\s]*$").WithMessage("'{PropertyName}' should only contain letters.");

            RuleFor(a => a.Password)
                   .NotEmpty()
                   .MinimumLength(8)
                   .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                   .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                   .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                   .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");

            /*
          * Pembuatan role pada password dengan berbagai ketentuan
          * password harus mengandung setidaknya
          * 1 huruf besar, 1 huruf kecil, 1 angka, dan 1 simbol
          * 
          */
            RuleFor(a => a.ConfirmPassword)
                    .NotEmpty()
                    .MinimumLength(8)
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[\!\?\*\.]*$").WithMessage("Your password must contain at least one (!? *.).");

        }

    }
}
