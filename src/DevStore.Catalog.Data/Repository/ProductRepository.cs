using DevStore.Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevStore.Catalog.Data.Repository;

public class ProductRepository(CatalogContext context) : IProductRepository
{
    private readonly CatalogContext _context = context;

    public async Task<IEnumerable<Product?>> GetAll()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await _context.Products.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product?>> GetByCategory(int code)
    {
        return await _context.Products.AsNoTracking()
            .Include(p => p.Category)
            .Where(c => c.Category != null && c.Category.Code == code)
            .ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _context.Categories.AsNoTracking().ToListAsync();
    }

    public void Create(Product product)
    {
        _context.Products.Add(product);
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    public void Create(Category category)
    {
        _context.Categories.Add(category);
    }

    public void Update(Category category)
    {
        _context.Categories.Update(category);
    }
}
