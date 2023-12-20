using DevStore.Common.Data;

namespace DevStore.Catalog.Domain;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product?>> GetAll();
    Task<Product?> GetById(Guid id);
    Task<IEnumerable<Product?>> GetByCategory(int code);
    Task<IEnumerable<Category>> GetCategories();
    void Create(Product product);
    void Update(Product product);
    void Create(Category category);
    void Update(Category category);
}
