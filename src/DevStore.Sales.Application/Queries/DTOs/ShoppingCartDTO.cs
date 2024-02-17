namespace DevStore.Sales.Application.Queries.DTOs;

public class ShoppingCartDTO
{
    public Guid OrderId { get; set; }
    public Guid ClientId { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TotalValue { get; set; }
    public decimal Discount { get; set; }
    public string VoucherCode { get; set; } = string.Empty;

    public List<ShoppingCartItemDTO> Items { get; set; } = [];
    public ShoppingCartPaymentDTO Payment { get; set; } = new ShoppingCartPaymentDTO();
}