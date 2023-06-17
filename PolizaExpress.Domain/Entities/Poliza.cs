using PolizaExpress.Domain.Events.Polizas;
using PolizaExpress.SharedKernel;
using PolizaExpress.SharedKernel.Helpers;
using PolizaExpress.SharedKernel.ValueObjects;

namespace PolizaExpress.Domain.Entities;

public class Poliza : BaseEntity, IAggregateRoot
{
    public string NumeroPoliza { get; private set; }
    public Cliente Cliente { get; private set; }
    public DateTime FechaPoliza { get; private set; }
    public DateTime FechaVencimiento { get; private set; }
    public DateTime FechaInicioVigencia { get; private set; }
    public List<Cobertura> Coberturas { get; private set; } = new();
    public decimal ValorMaximoCubierto { get; private set; }
    public string NombrePlan { get; private set; }
    public Vehiculo Vehiculo { get; private set; }
    public bool TieneInspeccion { get; private set; }
    
    public Poliza(
        string plan,
        Cliente cliente,
        List<Cobertura> coberturas,
        Vehiculo vehiculo)
    {
        ArgumentNullException.ThrowIfNull(cliente);
        ArgumentNullException.ThrowIfNull(vehiculo);
        
        Id = Guid.NewGuid();
        NumeroPoliza = PolizaHelper.GenerarNumeroPoliza();
        Cliente =  cliente;
        Coberturas = coberturas;
        NombrePlan = plan;
        Vehiculo = vehiculo;
        TieneInspeccion = false;
        ValorMaximoCubierto = Coberturas.Sum(c => c.Valor);
        
        // Con esta logica se evita que se cree una poliza sin vigencia
        FechaPoliza = DateTime.Now;
        FechaInicioVigencia = FechaPoliza.AddDays(1);
        FechaVencimiento = FechaPoliza.AddYears(1);
        
        // Se agrego por requisito de la prueba
        if (!TieneVigencia())
        {
            throw new InvalidOperationException("No se puede crear una poliza sin vigencia.");
        }
        
        AddDomainEvent(new PolizaCreadaDomainEvent(
            Guid.NewGuid(), 
            Id, 
            NumeroPoliza,
            Cliente,
            Coberturas,
            ValorMaximoCubierto,
            NombrePlan,
            Vehiculo));
    }
    
    public bool TieneVigencia()
    {
        return FechaVencimiento >= DateTime.Now;
    }
}