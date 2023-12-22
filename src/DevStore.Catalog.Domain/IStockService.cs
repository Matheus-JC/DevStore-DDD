namespace DevStore.Catalog.Domain;

public interface IStockService : IDisposable
{
    Task<bool> DebitStock(Guid productId, int quantity);
    Task<bool> ReplenishStock(Guid productId, int quantity);
}
