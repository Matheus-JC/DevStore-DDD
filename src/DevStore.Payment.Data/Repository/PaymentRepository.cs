using DevStore.Payment.Business;

namespace DevStore.Payment.Data.Repository;

public class PaymentRepository(PaymentContext context) : IPaymentRepository
{
    private readonly PaymentContext _context = context;

    public void Add(PaymentItem payment)
    {
        _context.Payments.Add(payment);
    }

    public void AddTransaction(PaymentTransaction transaction)
    {
        _context.Transactions.Add(transaction);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
