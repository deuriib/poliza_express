using MediatR;
using PolizaExpress.SharedKernel.Dtos;

namespace PolizaExpress.Application.Polizas.Commands.Crear;

public class CrearPolizaCommand : IRequest<PolizaDto>
{
    public string Plan { get; init; }
    public ClienteDto Cliente { get; init; }
    public VehiculoDto Vehiculo { get; init; }
    public List<CoberturaDto> Coberturas { get; init; } = new();
}