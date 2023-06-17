using FluentValidation;
using PolizaExpress.SharedKernel.Dtos;

namespace PolizaExpress.Application.Polizas.Validators;

public sealed class VehiculoDtoValidator : AbstractValidator<VehiculoDto>
{
    public VehiculoDtoValidator()
    {
        RuleFor(x => x.Placa)
            .NotNull()
            .WithMessage("La placa no puede ser nula")
            .NotEmpty()
            .WithMessage("La placa no puede ser vacía")
            .MaximumLength(6)
            .WithMessage("La placa no puede tener más de 6 caracteres")
            .Matches("^[A-Z]{3}[0-9]{3}$")
            .WithMessage("La placa debe tener el formato ABC123");
        
        RuleFor(x => x.Modelo)
            .NotNull()
            .WithMessage("El modelo no puede ser nulo")
            .NotEmpty()
            .WithMessage("El modelo no puede ser vacío")
            .MaximumLength(4)
            .WithMessage("El modelo no puede tener más de 4 caracteres")
            .Matches("^[0-9]{4}$")
            .WithMessage("El modelo debe tener el formato 1234");
    }
}