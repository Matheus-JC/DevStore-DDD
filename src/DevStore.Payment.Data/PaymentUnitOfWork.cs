using DevStore.Common.Communication.Mediator;
using DevStore.Payment.Business;

namespace DevStore.Payment.Data;

public class PaymentUnitOfWork(PaymentContext context, IMediatorHandler mediatorHandler) : IPaymentUnitOfWork
{
    private readonly PaymentContext _context = context;
    private readonly IMediatorHandler _mediatorHandler = mediatorHandler;

    public async Task<bool> Commit()
    {
        var success = await _context.SaveChangesAsync() > 0;

        if (success)
            await _mediatorHandler.PublishEvents(_context);

        return success;
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
