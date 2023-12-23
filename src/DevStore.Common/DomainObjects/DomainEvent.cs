using DevStore.Common.Messages;

namespace DevStore.Common.DomainObjects;

public class DomainEvent : Event
{
    public DomainEvent(Guid aggregateId) 
    {
        AggregateId = aggregateId;
    }
}
