
using EadFacil.Core.Communication.Mediator;
using EadFacil.Core.DomainObjects;
using EadFacil.Core.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EadFacil.Core.Data.Extensions;

public static class MediatorExtensions
{
    public static async Task PublicarEventos<TContext>(this IMediatorHandler mediator, TContext context) 
        where TContext : DbContext
    {
        var entidadesComEventos = ObterEntidadesComEventos(context).ToList();
        if (!entidadesComEventos.Any()) 
        {return;}

        var eventosParaPublicar = ExtrairEventos(entidadesComEventos);
        LimparEventosPublicados(entidadesComEventos);
        await PublicarEventosAsync(mediator, eventosParaPublicar);
    }

    private static IEnumerable<EntityEntry<Entity>> ObterEntidadesComEventos<TContext>(TContext context) 
        where TContext : DbContext
    {
        return context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.EventsToPublish?.Any() == true)
            .ToList();
    }

    private static List<Event> ExtrairEventos(IEnumerable<EntityEntry<Entity>> entidades)
    {
        return entidades
            .SelectMany(x => x.Entity.EventsToPublish)
            .ToList();
    }

    private static void LimparEventosPublicados(IEnumerable<EntityEntry<Entity>> entidades)
    {
        foreach (var entity in entidades)
        {
            entity.Entity.ClearEvents();
        }
    }

    private static Task PublicarEventosAsync(IMediatorHandler mediator, IEnumerable<Event> eventos)
    {
        var tarefasPublicacao = eventos.Select(evento => mediator.PublicarEvento(evento));
        return Task.WhenAll(tarefasPublicacao);
    }
}