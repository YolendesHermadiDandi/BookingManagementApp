using API.DTOs.Educations;
using FluentValidation;

namespace API.Utilities.Validations.Educations
{
    public class CreateEducationValidator : AbstractValidator<CreateEducationDto>
    {
        public CreateEducationValidator()
        {

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


        }
    }
}
