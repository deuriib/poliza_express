using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PolizaExpress.Domain.Entities;

namespace PolizaExpress.Infrastructure.Autenticacion;

public class JwtService
{
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<JwtService> _logger;
    public JwtService(IOptions<JwtSettings> jwtSettingsOptions, ILogger<JwtService> logger)
    {
        _logger = logger;
        _jwtSettings = jwtSettingsOptions.Value;
    }

    public string GenerarToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Surname, usuario.Apellido),
            }),
            Audience = _jwtSettings.Audience,
            Issuer = _jwtSettings.Issuer,
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}