namespace PolizaExpress.SharedKernel.Dtos;

public record PolizaDto(
    Guid Id,
    string NumeroPoliza,
    string Plan,
    decimal ValorMaximoCubierto,
    DateTime FechaInicioVigencia,
    DateTime FechaFinVigencia,
    ClienteDto Cliente,
    VehiculoDto Vehiculo,
    List<CoberturaDto> Coberturas);