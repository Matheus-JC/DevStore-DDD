using DevStore.Common.Data;

namespace DevStore.Catalog.Data.UnitOfWork;

public class UnitOfWork(CatalogContext context) : IUnitOfWork
{
    private readonly CatalogContext _context = context;

    public async Task<bool> Commit()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
