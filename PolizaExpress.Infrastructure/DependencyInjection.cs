using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoFramework;
using PolizaExpress.Infrastructure.Autenticacion;
using PolizaExpress.Infrastructure.Context;

namespace PolizaExpress.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddScoped<AppDbContext>(sp =>
        {
            var connection = MongoDbConnection.FromConnectionString(
                configuration.GetConnectionString("MongoDb"));

            var publisher = sp.GetRequiredService<IPublisher>();

            return new AppDbContext(connection, publisher);
        });
        
        services.AddOptions<JwtSettings>()
            .BindConfiguration(nameof(JwtSettings))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddScoped<JwtService>();

        return services;
    }
}