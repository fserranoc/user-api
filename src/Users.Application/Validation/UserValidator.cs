using FluentValidation;
using static Users.Application.Dtos.UserDtos;

namespace Users.Application.Validation
{
    public sealed class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("Nombre de usuario es obligatorio").MaximumLength(100);
            RuleFor(x => x.Apellido).NotEmpty().WithMessage("Apellido es obligatorio").MaximumLength(100);
            RuleFor(x => x.Correo).NotEmpty().WithMessage("Correo es obligatorio").EmailAddress().MaximumLength(150);
        }
    }


    public sealed class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.IdUsuario).GreaterThan(0).WithMessage("EL id de usuario debe ser mayor que 0.");
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("Nombre de usuario es obligatorio").MaximumLength(100);
            RuleFor(x => x.Apellido).NotEmpty().WithMessage("Apellido es obligatorio").MaximumLength(100);
            RuleFor(x => x.Correo).NotEmpty().WithMessage("Correo es obligatorio").EmailAddress().WithMessage("El formato del correo no es correcto.").MaximumLength(150);
        }
    }
}
