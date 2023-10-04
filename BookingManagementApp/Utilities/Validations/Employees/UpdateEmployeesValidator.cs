using API.DTOs.Employee;
using FluentValidation;

namespace API.Utilities.Validations.Employees
{
    public class UpdateEmployeesValidator : AbstractValidator<EmployeeDto>
    {
        public UpdateEmployeesValidator()
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
                .GreaterThanOrEqualTo(DateTime.Now.AddYears(-18));

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

        }
    }
}
