using PolizaExpress.SharedKernel;

namespace PolizaExpress.Domain.Events.Usuario;

public record UsuarioCreadoEvent(
    Guid Id,
    Guid UsuarioId,
    string Correo,
    string Nombre,
    string Apellido
) : DomainEvent(Id);