using DevStore.Common.Data.EventSourcing;
using DevStore.Common.Messages;
using EventStore.ClientAPI;
using System.Text.Json;
using System.Text;

namespace EventSourcing;

public class EventSourcingRepository(IEventStoreService eventStoreService) : IEventSourcingRepository
{
    private readonly IEventStoreService _eventStoreService = eventStoreService;

    public async Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId)
    {
        var events = await _eventStoreService.GetConnection()
            .ReadStreamEventsForwardAsync(
                stream: aggregateId.ToString(),
                start: 0,
                count: 500,
                resolveLinkTos: false);

        var eventsList = new List<StoredEvent>();

        foreach (var resolvedEvent in events.Events)
        {
            var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
            var jsonData = JsonSerializer.Deserialize<BaseEvent>(dataEncoded);

            if (jsonData is not null)
            {
                var eventItem = new StoredEvent(
                    resolvedEvent.Event.EventId,
                    resolvedEvent.Event.EventType,
                    jsonData.Timestamp,
                    dataEncoded
                );

                eventsList.Add(eventItem);
            }
        }

        return eventsList.OrderBy(e => e.OccurrenceDate);
    }

    public async Task SaveEvent<TEvent>(TEvent eventItem) where TEvent : Event
    {
        await _eventStoreService.GetConnection().AppendToStreamAsync(
            eventItem.AggregateId.ToString(),
            ExpectedVersion.Any,
            FormatEvent(eventItem));
    }

    private static IEnumerable<EventData> FormatEvent<TEvent>(TEvent eventItem) where TEvent : Event
    {
        yield return new EventData(
            eventId: Guid.NewGuid(),
            type: eventItem.MessageType,
            isJson: true,
            data: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(eventItem)),
            metadata: null);
    }
}
