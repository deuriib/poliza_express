using MediatR;
using Microsoft.Extensions.Logging;
using PolizaExpress.Domain.Events.Polizas;

namespace PolizaExpress.Application.Polizas.Commands.Crear;

public sealed class PolizaCreadaDomainEventHandler : INotificationHandler<PolizaCreadaDomainEvent>
{
    private readonly ILogger<PolizaCreadaDomainEventHandler> _logger;

    public PolizaCreadaDomainEventHandler(ILogger<PolizaCreadaDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(PolizaCreadaDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"📄Poliza creada: {notification.NumeroPoliza}");
        return Task.CompletedTask;
    }
}