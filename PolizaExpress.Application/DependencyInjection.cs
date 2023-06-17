using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PolizaExpress.Application.Polizas.Commands.Crear;
using PolizaExpress.Domain.Events.Polizas;
using PolizaExpress.SharedKernel;

namespace PolizaExpress.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssemblies(
                typeof(CrearPolizaCommand).Assembly,
                typeof(PolizaCreadaDomainEvent).Assembly,
                typeof(DomainEvent).Assembly
            );
        });

        // FluentValidation
        services.AddValidatorsFromAssemblyContaining<CrearPolizaCommandValidator>();

        return services;
    }
}