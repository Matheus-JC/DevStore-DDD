using DevStore.Common.DomainObjects;

namespace DevStore.Sales.Domain;

public class Order : Entity, IAggregateRoot
{
    public int Code { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid? VoucherId { get; private set; }
    public bool VoucherUsed { get; private set; } = false;
    public decimal Discount { get; private set; }
    public decimal TotalValue { get; private set; }
    public DateTime CreationDate { get; private set; } = DateTime.UtcNow;
    public OrderStatus OrderStatus { get; private set; }

    private readonly List<OrderItem> _orderItems = [];
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public const int MaxOrderItemQuantity = 15;

    public Voucher? Voucher { get; private set; }

    // EF
    private Order() { }

    public Order(Guid clientId, decimal discount, decimal totalValue)
    {
        ClientId = clientId;
        Discount = discount;
        TotalValue = totalValue;
    }

    public DomainValidationResult ApplyVoucher(Voucher voucher)
    {
        var result = voucher.IsApplicable();

        if (!result.IsValid)
            return result;

        Voucher = voucher;
        Voucher.UpdateVoucherUsageData();
        VoucherUsed = true;
        CalculateOrderValue();

        return result;
    }

    public void CalculateOrderValue()
    {
        TotalValue = _orderItems.Sum(i => i.CalculateValue());
        CalculateTotalDiscountValue();
    }

    public void CalculateTotalDiscountValue()
    {
        if(!VoucherUsed || Voucher is null) 
            return;

        var discount = Voucher.DiscountType switch
        {
            VoucherDiscountType.Percentage => CalculatePercentagemDiscountValue(),
            VoucherDiscountType.Fixed => Voucher.DiscountValue,
            _ => 0
        };

        var totalValue = TotalValue -= discount;

        TotalValue = totalValue < 0 ? 0 : totalValue;
        Discount = discount;
    }

    private decimal CalculatePercentagemDiscountValue()
    {
        if(Voucher is null)
            return 0;

        return TotalValue * Voucher.DiscountValue / 100;
    }

    public bool OrderItemExists(OrderItem item)
    {
        return _orderItems.Any(i => i.ProductId == item.ProductId);
    }

    public void AddItem(OrderItem item)
    {
        item.AssociateOrder(Id);

        if (OrderItemExists(item))
        {
            var existingItem = GetExistingItem(item);
            existingItem.AddUnits(item.Quantity);
            item = existingItem;

            _orderItems.Remove(existingItem);
        }

        item.CalculateValue();
        _orderItems.Add(item);

        CalculateOrderValue();
    }

    public void RemoveItem(OrderItem item)
    {
        var existingItem = GetExistingItem(item);
        _orderItems.Remove(existingItem);

        CalculateOrderValue();
    }

    public OrderItem GetExistingItem(OrderItem item)
    {
        return _orderItems.FirstOrDefault(i => i.ProductId == item.ProductId) 
            ?? throw new DomainException("Item doesn't belong to ther order");
    }

    public void UpdateItem(OrderItem item)
    {
        item.AssociateOrder(Id);

        var existingItem = GetExistingItem(item);

        _orderItems.Remove(existingItem);
        _orderItems.Add(item);

        CalculateOrderValue();
    }

    public void UpdateUnits(OrderItem item, int units)
    {
        item.UpdateQuantity(units);
        UpdateItem(item);
    }

    public void MakeDraft()
    {
        OrderStatus = OrderStatus.Draft;
    }

    public void StartOrder()
    {
        OrderStatus = OrderStatus.Started;
    }

    public void FinalizeOrder()
    {
        OrderStatus = OrderStatus.Paid;
    }

    public void CancelOrder()
    {
        OrderStatus = OrderStatus.Canceled;
    }

    public static class OrderFactory
    {
        public static Order NewDraftOrder(Guid clientId)
        {
            var order = new Order
            {
                ClientId = clientId
            };

            order.MakeDraft();
            return order;
        }
    }
}
