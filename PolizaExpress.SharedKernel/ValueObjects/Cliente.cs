using MongoFramework;
using MongoFramework.Attributes;

namespace PolizaExpress.SharedKernel.ValueObjects;

public record Cliente
{
    public Cliente(string Nombre,
        string Identificacion,
        DateTime FechaNacimiento,
        string Ciudad,
        string Direccion)
    {
        if (string.IsNullOrEmpty(Nombre))
        {
            throw new ArgumentNullException(nameof(Nombre));
        }

        if (string.IsNullOrEmpty(Identificacion))
        {
            throw new ArgumentNullException(nameof(Identificacion));
        }

        if (string.IsNullOrEmpty(Ciudad))
        {
            throw new ArgumentNullException(nameof(Ciudad));
        }

        if (string.IsNullOrEmpty(Direccion))
        {
            throw new ArgumentNullException(nameof(Direccion));
        }

        if (string.IsNullOrEmpty(Nombre))
        {
            throw new ArgumentNullException(nameof(Nombre));
        }

        ArgumentNullException.ThrowIfNull(FechaNacimiento);

        this.Nombre = Nombre;
        this.Identificacion = Identificacion;
        this.FechaNacimiento = FechaNacimiento;
        this.Ciudad = Ciudad;
        this.Direccion = Direccion;

        if (!EsMayorDeEdad())
        {
            throw new InvalidOperationException("El cliente debe ser mayor de edad.");
        }

        if (EsMuyMayorParaConducir())
        {
            throw new InvalidOperationException("El cliente es muy mayor para conducir.");
        }
    }

    public string Nombre { get; init; }
    public string Identificacion { get; init; }
    public DateTime FechaNacimiento { get; init; }
    public string Ciudad { get; init; }
    public string Direccion { get; init; }

    private bool EsMayorDeEdad()
    {
        return CalcularEdad(FechaNacimiento, DateTime.Now) >= 18;
    }

    private bool EsMuyMayorParaConducir()
    {
        return CalcularEdad(FechaNacimiento, DateTime.Now) >= 80;
    }

    private int CalcularEdad(DateTime birthDate, DateTime now)
    {
        int edad = now.Year - birthDate.Year;

        if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            edad--;

        return edad;
    }

    public void Deconstruct(out string Nombre, out string Identificacion, out DateTime FechaNacimiento,
        out string Ciudad, out string Direccion)
    {
        Nombre = this.Nombre;
        Identificacion = this.Identificacion;
        FechaNacimiento = this.FechaNacimiento;
        Ciudad = this.Ciudad;
        Direccion = this.Direccion;
    }
}