using FluentValidation;

namespace PolizaExpress.Application.Usuarios.Registrar;

public sealed class RegistrarUsuarioCommandValidator : AbstractValidator<RegistrarUsuarioCommand>
{
    public RegistrarUsuarioCommandValidator()
    {
        RuleFor(x => x.Nombre)
            .NotNull()
            .WithMessage("El nombre no puede ser nulo")
            .NotEmpty()
            .WithMessage("El nombre es requerido")
            .MaximumLength(50)
            .WithMessage("El nombre no puede tener más de 50 caracteres");
        
        RuleFor(x => x.Apellido)
            .NotNull()
            .WithMessage("El apellido no puede ser nulo")
            .NotEmpty()
            .WithMessage("El apellido es requerido")
            .MaximumLength(50)
            .WithMessage("El apellido no puede tener más de 50 caracteres");
        
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
            .WithMessage("La contraseña debe tener al menos 10 caracteres, una mayúscula, una minúscula, un número y un caracter especial");
        
        RuleFor(x => x.ConfirmarPassword)
            .NotNull()
            .WithMessage("La confirmación de la contraseña no puede ser nula")
            .NotEmpty()
            .WithMessage("La confirmación de la contraseña es requerida")
            .Equal(x => x.Password)
            .WithMessage("Las contraseñas no coinciden");
    }   
}