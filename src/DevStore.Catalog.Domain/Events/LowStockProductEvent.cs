using DevStore.Common.Messages.CommonMessages.DomainEvents;

namespace DevStore.Catalog.Domain.Events;

public class LowStockProductEvent(Guid aggregateId, int remainingQuantity) : DomainEvent(aggregateId)
{
    public int RemainingQuantity { get; set; } = remainingQuantity;
}
