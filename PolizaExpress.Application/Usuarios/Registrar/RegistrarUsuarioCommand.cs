using MediatR;
using PolizaExpress.Application.Usuarios.Loguear;

namespace PolizaExpress.Application.Usuarios.Registrar;

public class RegistrarUsuarioCommand : IRequest
{
    public string Correo { get; set; }
    public string Password { get; set; }
    public string ConfirmarPassword { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
}