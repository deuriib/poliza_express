using PolizaExpress.Domain.Entities;
using PolizaExpress.SharedKernel;
using PolizaExpress.SharedKernel.ValueObjects;

namespace PolizaExpress.Domain.Events.Polizas;

public record PolizaCreadaDomainEvent(
        Guid Id, 
        Guid PolizaId, 
        string NumeroPoliza,
        Cliente PolizaCliente, 
        List<Cobertura> PolizaCoberturas, 
        decimal PolizaValorMaximoCubierto, 
        string PolizaNombrePlan, 
        Vehiculo PolizaVehiculo) 
    : DomainEvent(Id);