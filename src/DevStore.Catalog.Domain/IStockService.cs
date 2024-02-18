using DevStore.Common.DomainObjects.DTOs.Order;

namespace DevStore.Catalog.Domain;

public interface IStockService : IDisposable
{
    Task<bool> DebitStock(Guid productId, int quantity);
    Task<bool> DebitStockFromOrderProductsCollection(OrderProductsCollectionDTO products);
    Task<bool> ReplenishStock(Guid productId, int quantity);
    Task<bool> ReplenishStockFromOrderProductsCollection(OrderProductsCollectionDTO products);
}
