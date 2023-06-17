using System.ComponentModel.DataAnnotations;

namespace PolizaExpress.Infrastructure.Autenticacion;

public sealed class JwtSettings
{
    [Required]
    public string SecretKey { get; init; }
    
    [Required, DataType(DataType.Url)]
    public string Issuer { get; init; }
    
    [Required, DataType(DataType.Url)]
    public string Audience { get; init; }
    
    [Required]
    public int ExpirationInMinutes { get; init; }
}