using DevStore.Common.Data;

namespace DevStore.Payment.Business;

public interface IPaymentRepository : IRepository<PaymentItem>
{
    void Add(PaymentItem payment);

    void AddTransaction(PaymentTransaction transaction);
}
