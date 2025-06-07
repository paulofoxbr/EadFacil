using EadFacil.Core.Communication.Mediator;
using EadFacil.Core.DomainObjects;
using EadFacil.Core.Messages;
using EadFacil.Core.Messages.CommonMessages.DomainEvents;

namespace EadFacil.BC.Conteudo.Data;

public static class MediatorExtensions
{
    public static Task PublicarEventos(this IMediatorHandler mediator, DbContextConteudo dbContextConteudo)
    {
        var domanainEntities = dbContextConteudo.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.EventsToPublish != null && x.Entity.EventsToPublish.Any());
        
        var domainEvents = domanainEntities
            .SelectMany(x => x.Entity.EventsToPublish)
            .ToList();
        
        domanainEntities.ToList()
            .ForEach(entity=>entity.Entity.ClearEvents());
        
        var tasks = domainEvents.Select(async (domainEvent) =>
        { await mediator.PublicarEvento(domainEvent); });
        
        return Task.WhenAll(tasks);
    }

    public static Task PublishEvent(this IMediatorHandler mediator, DbContextConteudo dbContextConteudo)
    {
        var entidadesComEventos = dbContextConteudo.ObterEntidadesComEventos();
        var eventosParaPublicar = ExtrairEventosParaPublicar(entidadesComEventos);
        
        var tasks = eventosParaPublicar
            .Select(async evento => await mediator.PublicarEvento(evento));
        
        return Task.WhenAll(tasks);
    }

    private static IEnumerable<Event> ExtrairEventosParaPublicar(IEnumerable<Entity> entidadesComEventos)
    {
        return entidadesComEventos
            .SelectMany(entity => entity.EventsToPublish)
            .ToList();
    }

    private static IEnumerable<Entity> ObterEntidadesComEventos(this DbContextConteudo dbContextConteudo)
    {
        return dbContextConteudo.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.EventsToPublish != null && x.Entity.EventsToPublish.Any())
            .Select(x => x.Entity);
    }

}