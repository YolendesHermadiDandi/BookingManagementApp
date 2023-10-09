using API.DTOs.University;
using FluentValidation;

namespace API.Utilities.Validations.University
{
    public class CreateUniversityValidator : AbstractValidator<CreateUniversityDto>
    {
        public CreateUniversityValidator()
        {
            /*validator untuk code dengan ketentuan sbb:
              * 1. tidak boleh kosong
              * 2. panjang maksimum 50
              */
            RuleFor(u => u.Code)
                .NotEmpty()
                .MaximumLength(50);

            /*validator untuk name dengan ketentuan sbb:
            * 1. tidak boleh kosong
            * 2. panjang maksimum 100
            * 3. harus mengandung huruf
            */
            RuleFor(r => r.Name)
               .NotEmpty()
               .MaximumLength(100)
               .Matches(@"^[A-Za-z\s]*$").WithMessage("'{PropertyName}' should only contain letters.");


        }
    }
}
