using API.DTOs.Role;
using FluentValidation;

namespace API.Utilities.Validations.Roles
{
    public class CreateRolesValidator : AbstractValidator<CreateRoleDto>
    {
        public CreateRolesValidator()
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

        }
    }
}
