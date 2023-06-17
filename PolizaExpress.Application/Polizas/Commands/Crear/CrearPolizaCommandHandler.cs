using MediatR;
using PolizaExpress.Domain.Entities;
using PolizaExpress.Infrastructure.Context;
using PolizaExpress.SharedKernel.Dtos;
using PolizaExpress.SharedKernel.ValueObjects;

namespace PolizaExpress.Application.Polizas.Commands.Crear;

public sealed class CrearPolizaCommandHandler : IRequestHandler<CrearPolizaCommand, PolizaDto>
{
    private readonly AppDbContext _dbContext;

    public CrearPolizaCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PolizaDto> Handle(CrearPolizaCommand request, CancellationToken cancellationToken)
    {
        var poliza =  new Poliza(
            request.Plan,
            new Cliente(request.Cliente.Nombre,
                request.Cliente.Identificacion,
                request.Cliente.FechaNacimiento,
                request.Cliente.Ciudad,
                request.Cliente.Direccion),
            request.Coberturas
                .Select(c =>
                    Cobertura.Create(c.Codigo, c.Nombre, c.Descripcion, c.Valor))
                .ToList(),
            new Vehiculo(request.Vehiculo.Placa, request.Vehiculo.Modelo)
        );

        _dbContext.Polizas.Add(poliza);

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return new PolizaDto(
            poliza.Id,
            poliza.NumeroPoliza,
            poliza.NombrePlan, 
            poliza.ValorMaximoCubierto,
            poliza.FechaInicioVigencia,
            poliza.FechaVencimiento,
            new ClienteDto(poliza.Cliente.Nombre, 
                poliza.Cliente.Identificacion, 
                poliza.Cliente.FechaNacimiento, 
                poliza.Cliente.Ciudad, 
                poliza.Cliente.Direccion),
            new VehiculoDto(poliza.Vehiculo.Placa, poliza.Vehiculo.Modelo),
            poliza.Coberturas.Select(c => 
                new CoberturaDto(c.Codigo, c.Nombre, c.Descripcion, c.Valor))
                .ToList());
    }
}