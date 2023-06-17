using FluentValidation;
using PolizaExpress.SharedKernel.Dtos;

namespace PolizaExpress.Application.Polizas.Validators;

public sealed class ClienteDtoValidator : AbstractValidator<ClienteDto>
{
    public ClienteDtoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotNull()
            .WithMessage("El nombre no puede ser nulo")
            .NotEmpty()
            .WithMessage("El nombre no puede estar vacío")
            .MaximumLength(50)
            .WithMessage("El nombre no puede tener más de 50 caracteres")
            ;

        RuleFor(x => x.Identificacion)
            .NotNull()
            .WithMessage("La identificación no puede ser nula")
            .NotEmpty()
            .WithMessage("La identificación no puede estar vacía")
            .MaximumLength(11)
            .WithMessage("La identificación no puede tener más de 11 caracteres");
        
        RuleFor(x => x.Ciudad)
            .NotNull()
            .WithMessage("La ciudad no puede ser nula")
            .NotEmpty()
            .WithMessage("La ciudad no puede estar vacía")
            .MaximumLength(50)
            .WithMessage("La ciudad no puede tener más de 50 caracteres");

        RuleFor(x => x.Direccion)
            .NotEmpty()
            .WithMessage("La dirección no puede estar vacía")
            .MaximumLength(100)
            .WithMessage("La dirección no puede tener más de 100 caracteres");
    }
}