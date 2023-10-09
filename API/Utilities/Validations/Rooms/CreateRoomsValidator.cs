using API.DTOs.Room;
using FluentValidation;

namespace API.Utilities.Validations.Rooms
{
    public class CreateRoomsValidator : AbstractValidator<CreateRoomDto>
    {
        public CreateRoomsValidator()
        {
            /*validator untuk name dengan ketentuan sbb:
             * 1. tidak boleh kosong
             * 2. panjang maksimum 100
             * 3. harus mengandung huruf
             */
            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(100)
                .Matches(@"^[A-Za-z\s]*$").WithMessage("'{PropertyName}' should only contain letters.");

            /*validator untuk floor dengan ketentuan sbb:
              * 1. tidak boleh kosong
              * 2. angka yang dimasukan tidak boleh kurang dari 1
              */
            RuleFor(r => r.Floor)
                .NotEmpty()
                .GreaterThan(0);

            /*validator untuk capacity name dengan ketentuan sbb:
             * 1. tidak boleh kosong
             * 2. angka yang dimasukan tidak boleh kurang dari 1
             */
            RuleFor(r => r.Capacity)
                .NotEmpty()
                 .GreaterThan(0);

        }

    }
}
