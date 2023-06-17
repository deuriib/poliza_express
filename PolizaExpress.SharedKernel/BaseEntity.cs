using System.ComponentModel.DataAnnotations.Schema;

namespace PolizaExpress.SharedKernel;

public abstract class BaseEntity
{
    private readonly List<DomainEvent> _domainEvents = new();
    public Guid Id { get; protected set; }

    [NotMapped] public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents;

    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}