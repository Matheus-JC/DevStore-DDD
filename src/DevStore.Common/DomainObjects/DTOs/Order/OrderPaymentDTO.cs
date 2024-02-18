namespace DevStore.Common.DomainObjects.DTOs.Order;

public class OrderPaymentDTO
{
    public Guid OrderId { get; set; }
    public Guid ClientId { get; set; }
    public decimal TotalValue { get; set; }
    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string CardExpiration { get; set; }
    public string CardCvv { get; set; }
}
