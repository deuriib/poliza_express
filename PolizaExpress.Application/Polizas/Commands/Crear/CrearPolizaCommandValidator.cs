using FluentValidation;
using PolizaExpress.Application.Polizas.Commands.Crear;
using PolizaExpress.Application.Polizas.Validators;

namespace PolizaExpress.Application.Polizas.Commands.Radicar;

public class CrearPolizaCommandValidator : AbstractValidator<CrearPolizaCommand>
{
    public CrearPolizaCommandValidator()
    {
        RuleFor(x => x.Plan)
            .NotEmpty()
            .WithMessage("El plan es requerido");
        
        RuleFor(x => x.Cliente)
            .SetValidator(new ClienteDtoValidator());
        
        RuleFor(x => x.Vehiculo)
            .SetValidator(new VehiculoDtoValidator());
        
        RuleFor(x => x.Coberturas)
            .ForEach(a => 
                a.SetValidator(new CoberturaDtoValidator()));
    }
}