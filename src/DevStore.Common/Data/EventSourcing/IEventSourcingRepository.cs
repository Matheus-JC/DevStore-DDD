using DevStore.Common.Messages;

namespace DevStore.Common.Data.EventSourcing;

public interface IEventSourcingRepository
{
    Task SaveEvent<TEvent>(TEvent eventItem) where TEvent : Event;
    Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId);
}
