using DevStore.Common.Data;

namespace DevStore.Catalog.Domain;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product> GetById(Guid id);
    Task<Product> GetByCategory(int codigo);
    void Create(Product product);
    void Update(Product product);

    Task<IEnumerable<Category>> GetCategories();
    void Create(Category category);
    void Update(Category category);
}
