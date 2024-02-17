using DevStore.Catalog.Domain;

namespace DevStore.Catalog.Data;

public class CatalogUnitOfWork(CatalogContext context) : ICatalogUnitOfWork
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
