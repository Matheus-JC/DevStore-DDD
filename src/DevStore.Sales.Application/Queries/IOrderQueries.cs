using DevStore.Sales.Application.Queries.DTOs;

namespace DevStore.Sales.Application.Queries;

public interface IOrderQueries
{
    Task<ShoppingCartDTO?> GetShoppingCartClient(Guid clientId);
    Task<IEnumerable<OrderDTO>> GetOrdersClient(Guid clientId);
}
