using FluentValidation;

namespace PolizaExpress.Application.Usuarios.Loguear;

public sealed class LoguearUsuarioCommandValidator : AbstractValidator<LoguearUsuarioCommand>
{
    public LoguearUsuarioCommandValidator()
    {
        RuleFor(x => x.Correo)
            .NotNull()
            .WithMessage("El correo no puede ser nulo")
            .NotEmpty()
            .WithMessage("El correo es requerido")
            .EmailAddress()
            .WithMessage("El correo no es válido");
        
        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage("La contraseña no puede ser nula")
            .NotEmpty()
            .WithMessage("La contraseña es requerida")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{10,}$")
            .WithMessage(
                "La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula, un número y un caracter especial");
    }
}