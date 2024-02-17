using DevStore.Sales.Application.Queries.DTOs;
using DevStore.Sales.Domain;

namespace DevStore.Sales.Application.Queries;

public class OrderQueries(IOrderRepository orderRepository) : IOrderQueries
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<ShoppingCartDTO?> GetShoppingCartClient(Guid clientId)
    {
        var order = await _orderRepository.GetDraftOrderByClientId(clientId);

        if (order is null)
            return null;

        var shoppingCart = new ShoppingCartDTO
        {
            ClientId = clientId,
            TotalValue = order.TotalValue,
            OrderId = order.Id,
            Discount = order.Discount,
            SubTotal = order.Discount + order.TotalValue
        };

        if(order.Voucher is not null && order.VoucherId is not null)
        {
            shoppingCart.VoucherCode = order.Voucher.Code;
        }

        foreach(var item in order.OrderItems)
        {
            shoppingCart.Items.Add(new ShoppingCartItemDTO
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitValue = item.UnitValue,
                TotalValue = item.UnitValue * item.Quantity
            });
        }

        return shoppingCart;
    }

    public async Task<IEnumerable<OrderDTO>> GetOrdersClient(Guid clientId)
    {
        var orders = await _orderRepository.GetListByClientId(clientId);

        orders = orders.Where(o => o.OrderStatus == OrderStatus.Paid || o.OrderStatus == OrderStatus.Canceled)
            .OrderByDescending(o => o.Code);

        if (!orders.Any())
            return [];

        var ordersDto = new List<OrderDTO>();

        foreach(var order in orders)
        {
            ordersDto.Add(new OrderDTO
            {
                TotalValue = order.TotalValue,
                OrderStatus = (int) order.OrderStatus,
                Code = order.Code,
                CreationDate = order.CreationDate
            });
        }

        return ordersDto;
    }
}
