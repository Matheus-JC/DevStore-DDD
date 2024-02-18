using DevStore.Common.DomainObjects;

namespace DevStore.Payment.Business;

public class PaymentTransaction : Entity
{
    public Guid OrderId { get; set; }
    public Guid PaymentId { get; set; }
    public decimal TotalValue { get; set; }
    public PaymentTransactionStatus StatusTransaction { get; set; }

    public PaymentItem Payment {  get; set; }
}
