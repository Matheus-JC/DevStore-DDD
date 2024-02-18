using DevStore.Common.DomainObjects;

namespace DevStore.Payment.Business;

public class PaymentItem : Entity, IAggregateRoot
{
    public Guid OrderId { get; set; }
    public string? Status { get; set; }
    public decimal TotalValue { get; set; }

    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string CardExpiration { get; set; }
    public string CardCvv { get; set; }

    public PaymentTransaction Transaction { get; set; }
}
