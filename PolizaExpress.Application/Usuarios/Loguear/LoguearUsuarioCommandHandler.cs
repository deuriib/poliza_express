using MediatR;
using MongoFramework.Linq;
using PolizaExpress.Infrastructure.Autenticacion;
using PolizaExpress.Infrastructure.Context;

namespace PolizaExpress.Application.Usuarios.Loguear;

public sealed class LoguearUsuarioCommandHandler 
    : IRequestHandler<LoguearUsuarioCommand, UsuarioResponse>
{
    private readonly AppDbContext _dbContext;
    private readonly JwtService _jwtService;

    public LoguearUsuarioCommandHandler(AppDbContext dbContext, JwtService jwtService)
    {
        _dbContext = dbContext;
        _jwtService = jwtService;
    }

    public async Task<UsuarioResponse> Handle(LoguearUsuarioCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Usuarios
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Correo == request.Correo, cancellationToken);

        if (user is null)
        {
            throw new InvalidOperationException(
                "el usuario no existe, por favor verifique los datos ingresados");
        }
        
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHashed))
        {
            throw new InvalidOperationException(
                "Ha ocurrido un error al intentar autenticar el usuario");
        }
        
        var token = _jwtService.GenerarToken(user);
        
        return new UsuarioResponse
        {
            AccessToken = token,
        };
    }
}