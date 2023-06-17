using MediatR;
using MongoFramework.Linq;
using PolizaExpress.Application.Usuarios.Loguear;
using PolizaExpress.Domain.Entities;
using PolizaExpress.Domain.Events.Usuario;
using PolizaExpress.Infrastructure.Autenticacion;
using PolizaExpress.Infrastructure.Context;

namespace PolizaExpress.Application.Usuarios.Registrar;

public sealed class RegistrarUsuarioCommandHandler 
    : IRequestHandler<RegistrarUsuarioCommand>
{
    private readonly AppDbContext _dbContext;
    private readonly JwtService _jwtService;
    public RegistrarUsuarioCommandHandler(AppDbContext dbContext, JwtService jwtService)
    {
        _dbContext = dbContext;
        _jwtService = jwtService;
    }
    public async Task Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _dbContext.Usuarios.
            AsNoTracking()
            .SingleOrDefaultAsync(u => u.Correo == request.Correo, cancellationToken);

        if (usuario is not null)
        {
            throw new InvalidOperationException("Ha ocurrido un error al registrar el usuario");
        }
        
        var passwordHashed = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        var nuevoUsuario = new Usuario(
            request.Correo,
            passwordHashed,
            request.Nombre,
            request.Apellido
        );
        
        _dbContext.Usuarios.Add(nuevoUsuario);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}