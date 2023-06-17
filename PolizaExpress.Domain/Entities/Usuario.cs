using PolizaExpress.Domain.Events.Usuario;
using PolizaExpress.SharedKernel;

namespace PolizaExpress.Domain.Entities;

public class Usuario : BaseEntity
{
    public string Correo { get; private set; }
    public string PasswordHashed { get; private set; }
    public string Nombre { get; private set; }
    public string Apellido { get; private set; }

    public Usuario(string correo, string passwordHashed, string nombre, string apellido)
    {
        Id = Guid.NewGuid();
        Correo = correo;
        PasswordHashed = passwordHashed;
        Nombre = nombre;
        Apellido = apellido;
        
        AddDomainEvent(new UsuarioCreadoEvent(
            Guid.NewGuid(),
            Id,
            Correo,
            Nombre,
            Apellido));
    }
}