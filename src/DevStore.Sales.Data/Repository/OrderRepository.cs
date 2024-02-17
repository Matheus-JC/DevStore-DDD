using DevStore.Sales.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevStore.Sales.Data.Repository;

public class OrderRepository(SalesContext context) : IOrderRepository
{
    // Order

    private readonly SalesContext _context = context;

    public async Task<Order?> GetById(Guid id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task<IEnumerable<Order>> GetListByClientId(Guid clientId)
    {
        return await _context.Orders.AsNoTracking().Where(p => p.ClientId == clientId).ToListAsync();
    }

    public async Task<Order?> GetDraftOrderByClientId(Guid clientId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(p => p.ClientId == clientId 
            && p.OrderStatus == OrderStatus.Draft);
        
        if (order is null) 
            return null;

        await _context.Entry(order)
            .Collection(i => i.OrderItems).LoadAsync();

        if (order.VoucherId != null)
        {
            await _context.Entry(order)
                .Reference(i => i.Voucher).LoadAsync();
        }

        return order;
    }

    public void Add(Order order)
    {
        _context.Orders.Add(order);
    }

    public void Update(Order order)
    {
        _context.Orders.Update(order);
    }

    // Order Item

    public async Task<OrderItem?> GetItemById(Guid id)
    {
        return await _context.OrderItems.FindAsync(id);
    }

    public async Task<OrderItem?> GetItemByOrder(Guid orderId, Guid productId)
    {
        return await _context.OrderItems.FirstOrDefaultAsync(p => p.OrderId == orderId 
            && p.ProductId == productId);
    }

    public void AddItem(OrderItem orderItem)
    {
        _context.OrderItems.Add(orderItem);
    }

    public void UpdateItem(OrderItem orderItem)
    {
        _context.OrderItems.Update(orderItem);
    }

    public void RemoveItem(OrderItem orderItem)
    {
        _context.OrderItems.Remove(orderItem);
    }

    // Voucher

    public async Task<Voucher?> GetVoucherByCode(string code)
    {
        return await _context.Vouchers.FirstOrDefaultAsync(p => p.Code == code);
    }

    public void UpdateVoucher(Voucher voucher)
    {
        _context.Vouchers.Update(voucher);
    }

    // General

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
