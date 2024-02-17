using DevStore.Common.Communication.Mediator;
using DevStore.Sales.Domain;

namespace DevStore.Sales.Data;

public class SalesUnitOfWork(SalesContext context, IMediatorHandler mediatorHandler) : ISalesUnitOfWork
{
    private readonly SalesContext _context = context;
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
