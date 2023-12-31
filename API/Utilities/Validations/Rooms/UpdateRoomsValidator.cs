﻿using API.DTOs.Room;
using FluentValidation;

namespace API.Utilities.Validations.Rooms
{
    public class UpdateRoomsValidator : AbstractValidator<RoomDto>
    {
        public UpdateRoomsValidator() 
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
                .LessThan(1);

            /*validator untuk capacity name dengan ketentuan sbb:
             * 1. tidak boleh kosong
             * 2. angka yang dimasukan tidak boleh kurang dari 1
             */
            RuleFor(r => r.Capacity)
                .NotEmpty()
                .LessThan(1);



        }
    }
}
