using API.DTOs.Booking;
using API.Utilities.Enums;
using FluentValidation;

namespace API.Utilities.Validations.Booking
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingDto>
    {
        public CreateBookingValidator()
        {
            //validasi start date tidak boleh kosong
            RuleFor(b => b.StartDate)
                .NotEmpty();
            //validasi end date tidak boleh kosong
            RuleFor(b => b.EndDate)
                .NotEmpty();
            //validasi ramarks tidak boleh kosong
            RuleFor(b => b.Remarks)
                .NotEmpty();

        }

    }
}
