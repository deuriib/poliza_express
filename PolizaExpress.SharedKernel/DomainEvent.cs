using MediatR;

namespace PolizaExpress.SharedKernel;

public abstract record DomainEvent(Guid Id) : INotification;