namespace DevStore.Payment.Business;

public class Order
{
    public Guid Id { get; set; }
    public decimal TotalValue { get; set; }
    public List<Product> Products { get; set; } = [];
}
