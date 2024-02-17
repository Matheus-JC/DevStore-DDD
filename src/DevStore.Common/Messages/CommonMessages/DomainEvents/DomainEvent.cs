namespace DevStore.Common.Messages.CommonMessages.DomainEvents;

public class DomainEvent : Event
{
    public DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}
