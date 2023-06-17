using MediatR;
using MongoFramework.Linq;
using PolizaExpress.Domain.Entities;
using PolizaExpress.Infrastructure.Context;
using PolizaExpress.SharedKernel.Dtos;

namespace PolizaExpress.Application.Polizas.Queries.Buscar;

public sealed class BuscarQueryHandler : IRequestHandler<BuscarQuery, IEnumerable<PolizaDto>?>
{
    private readonly AppDbContext _dbContext;

    public BuscarQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<PolizaDto>?> Handle(BuscarQuery request, CancellationToken cancellationToken)
    {
        var polizas = await _dbContext.Polizas.SearchText(request.Query)
            .ToListAsync(cancellationToken)!;

        if (polizas is null) return null;
        
        return polizas.Select(poliza => new PolizaDto(
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
                    .ToList()))
            .ToList();
    }
}