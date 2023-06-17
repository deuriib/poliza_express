using MediatR;
using PolizaExpress.SharedKernel.Dtos;

namespace PolizaExpress.Application.Polizas.Queries.Buscar;

public sealed class BuscarQuery : IRequest<IEnumerable<PolizaDto>?>
{
    public string Query { get; set; }
}