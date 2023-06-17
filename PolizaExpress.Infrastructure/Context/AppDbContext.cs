using MediatR;
using MongoFramework;
using PolizaExpress.Domain.Entities;
using PolizaExpress.SharedKernel;

namespace PolizaExpress.Infrastructure.Context;

public class AppDbContext : MongoDbContext
{
    private readonly IPublisher _publisher;

    public AppDbContext(IMongoDbConnection connection, IPublisher publisher)
        : base(connection)
    {
        _publisher = publisher;
    }

    public MongoDbSet<Poliza> Polizas { get; set; }
    public MongoDbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfigureMapping(MappingBuilder mappingBuilder)
    {
        mappingBuilder.Entity<Usuario>()
            .HasIndex(s => s.Correo, b =>
            {
                b.HasName("IX_Correo");
                b.IsUnique();
                b.IsDescending(true);
            });

        mappingBuilder.Entity<Poliza>()
            .HasIndex(
                p => new
                {
                    p.NumeroPoliza, 
                    p.Vehiculo.Placa
                },
                b =>
                {
                    b.HasName("IX_NumeroPoliza_Vehiculo_Placa");
                    b.HasType(IndexType.Text);
                    b.IsDescending(true, false);
                });
    }

    public override async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await DispatchEvents(this, cancellationToken);

        await base.SaveChangesAsync(cancellationToken);
    }

#region PRIVATE METHODS
    private Task DispatchEvents(MongoDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var domainEntities = dbContext.ChangeTracker
            .Entries()
            .Where(x => x.Entity is BaseEntity)
            .Select(x => x.Entity as BaseEntity)
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x!.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity!.ClearDomainEvents());

        var tasks = domainEvents
            .Select(async (domainEvent) =>
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            });

        return Task.WhenAll(tasks);
    }
    
#endregion
}