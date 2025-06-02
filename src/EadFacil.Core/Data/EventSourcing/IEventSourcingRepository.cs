using EadFacil.Core.Messages;

namespace EadFacil.Core.Data.EventSourcing;

public interface IEventSourcingRepository
{
    Task SaveEvent<T>(T eventToSave) where T : Event;
    Task<IEnumerable<StoredEvent>> GetEvent(Guid aggregateId);
}