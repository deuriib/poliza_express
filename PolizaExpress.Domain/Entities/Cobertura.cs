using PolizaExpress.SharedKernel;

namespace PolizaExpress.Domain.Entities;

public class Cobertura : BaseEntity
{
    public static Cobertura ResponsabilidadCivil =>
        new("RC", "Responsabilidad Civil", 
            "Cubre daños a terceros en caso de accidente", 
            1000);

    public static Cobertura CoberturaColision =>
        new Cobertura("CC", "Cobertura de Colisión", 
            "Cubre los daños al vehículo por colisión", 1500);

    public static Cobertura CoberturaIntegral => 
        new Cobertura("CI", "Cobertura Integral",
        "Cubre daños al vehículo no causados por colisión", 2000);

    public string Codigo { get; private set; }
    public string Nombre { get; private set; }
    public string Descripcion { get; private set; }
    public decimal Valor { get; private set; }

    private Cobertura(string codigo, string nombre, string descripcion, decimal valor)
    {
        Id = Guid.NewGuid();
        Codigo = codigo;
        Nombre = nombre;
        Descripcion = descripcion;
        Valor = valor;
    }
    
    public static Cobertura Create(string codigo, string nombre, string descripcion, decimal valor)
    {
        return new Cobertura(codigo, nombre, descripcion, valor);
    }
    
    public Cobertura Actualizar(string nombre, string descripcion, decimal valor)
    {
        return new Cobertura(Codigo, nombre, descripcion, valor);
    }
}