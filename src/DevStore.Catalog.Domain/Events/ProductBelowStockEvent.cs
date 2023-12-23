using DevStore.Common.DomainObjects;

namespace DevStore.Catalog.Domain.Events;

public class ProductBelowStockEvent(Guid aggregateId, int remainingQuantity) : DomainEvent(aggregateId)
{
    public int RemainingQuantity { get; set; } = remainingQuantity;
}
