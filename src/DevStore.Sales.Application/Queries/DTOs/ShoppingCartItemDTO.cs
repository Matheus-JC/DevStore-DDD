namespace DevStore.Sales.Application.Queries.DTOs;

public class ShoppingCartItemDTO
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitValue { get; set; }
    public decimal TotalValue { get; set; }
}
