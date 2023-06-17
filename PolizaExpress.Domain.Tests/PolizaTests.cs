using AutoBogus;
using PolizaExpress.Domain.Entities;
using PolizaExpress.Domain.Events.Polizas;
using PolizaExpress.SharedKernel.ValueObjects;

namespace PolizaExpress.Domain.Tests;

public class PolizaTests
{
    private Cliente _cliente;

    private List<Cobertura> _coberturas;

    private Vehiculo _vehiculo;

    // Prueba: Verificar que se cree una póliza con los parámetros válidos
    [Fact]
    public void Crear_poliza_con_parametros_validos()
    {
        // Arrange
        _cliente = AutoFaker.Generate<Cliente>();
        _coberturas = AutoFaker.Generate<List<Cobertura>>();
        _vehiculo = AutoFaker.Generate<Vehiculo>();

        // Act
        var poliza = new Poliza("Plan A", _cliente, _coberturas, _vehiculo);

        // Assert
        Assert.NotNull(poliza);
        Assert.NotEmpty(poliza.NumeroPoliza);
        Assert.Equal(_cliente, poliza.Cliente);
        Assert.Equal(_coberturas, poliza.Coberturas);
        Assert.Equal(_coberturas.Sum(c => c.Valor), poliza.ValorMaximoCubierto);
        Assert.Equal("Plan A", poliza.NombrePlan);
        Assert.Equal(_vehiculo, poliza.Vehiculo);
        Assert.False(poliza.TieneInspeccion);
        Assert.True(poliza.TieneVigencia());
        Assert.Single(poliza.DomainEvents);
        Assert.IsType<PolizaCreadaDomainEvent>(poliza.DomainEvents.First());
    }

    // Prueba: Verificar que se genere una excepción cuando se crea una póliza con un cliente nulo
    [Fact]
    public void Crear_poliza_con_cliente_nulo()
    {
        // Arrange
        _coberturas = AutoFaker.Generate<List<Cobertura>>();
        _vehiculo = AutoFaker.Generate<Vehiculo>();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            var poliza = new Poliza("Plan A", null, _coberturas, _vehiculo);
        });
    }
    
    // Prueba: Verificar que se genere un número de póliza no vacío
    [Fact]
    public void Poliza_Creation_GeneratesNonEmptyNumeroPoliza()
    {
        // Arrange
        _cliente = AutoFaker.Generate<Cliente>();
        _coberturas = AutoFaker.Generate<List<Cobertura>>();
        _vehiculo = AutoFaker.Generate<Vehiculo>();

        // Act
        var poliza = new Poliza("Plan A", _cliente, _coberturas, _vehiculo);

        // Assert
        Assert.NotEmpty(poliza.NumeroPoliza);
    }
    
    // Prueba: Verificar que la suma de los valores de las coberturas sea igual al valor máximo cubierto por la póliza
    [Fact]
    public void Poliza_Creation_ValidValorMaximoCubierto()
    {
        // Arrange
        _cliente = AutoFaker.Generate<Cliente>();
        _coberturas = AutoFaker.Generate<List<Cobertura>>();
        _vehiculo = AutoFaker.Generate<Vehiculo>();

        // Act
        var poliza = new Poliza("Plan A", _cliente, _coberturas, _vehiculo);
        var valorMaximoCubierto = _coberturas.Sum(c => c.Valor);
        
        // Assert
        Assert.Equal(valorMaximoCubierto, poliza.ValorMaximoCubierto);
    }
}