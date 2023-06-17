using FluentValidation;
using PolizaExpress.SharedKernel.Dtos;

namespace PolizaExpress.Application.Polizas.Validators;

public sealed class CoberturaDtoValidator : AbstractValidator<CoberturaDto>
{
    public CoberturaDtoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotNull()
            .WithMessage("El nombre de la cobertura no puede ser nulo")
            .NotEmpty()
            .WithMessage("El nombre de la cobertura no puede estar vacío")
            .MaximumLength(50)
            .WithMessage("El nombre de la cobertura no puede tener más de 50 caracteres")
            ;
        
        RuleFor(x => x.Descripcion)
            .NotNull()
            .WithMessage("La descripción de la cobertura no puede ser nula")
            .NotEmpty()
            .WithMessage("La descripción de la cobertura no puede estar vacía")
            .MaximumLength(100)
            .WithMessage("La descripción de la cobertura no puede tener más de 100 caracteres")
            ;
        
        RuleFor(x => x.Valor)
            .NotNull()
            .WithMessage("El valor de la cobertura no puede ser nulo")
            .NotEmpty()
            .WithMessage("El valor de la cobertura no puede estar vacío")
            .GreaterThan(0)
            .WithMessage("El valor de la cobertura debe ser mayor a 0")
            ;
    }
}