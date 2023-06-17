using MediatR;

namespace PolizaExpress.Application.Usuarios.Loguear;

public class LoguearUsuarioCommand : IRequest<UsuarioResponse>
{
    public string Correo { get; set; }
    public string Password { get; set; }
}