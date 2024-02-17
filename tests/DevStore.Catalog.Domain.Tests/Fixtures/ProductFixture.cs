using Bogus;
using Moq.AutoMock;

namespace DevStore.Catalog.Domain.Tests.Fixtures;

public class ProductFixture : IDisposable
{
    private readonly Faker _faker;
    public DimensionsFixture DimensionsFixture { get; private set; }

    public ProductFixture()
    {
        _faker = new Faker("en_US");
        DimensionsFixture = new DimensionsFixture();
    }

    public Product CreateValidProduct(bool active = true) =>
        GenerateValidProducts(quantity: 1, active: active).First();

    public Product CreateValidProductWithSpecifiedStock(int stock) =>
        GenerateValidProducts(quantity: 1, active: true, stock: stock).First();

    public List<Product> GenerateValidProducts(int quantity, bool active, int? stock = null)
    {
        var products = new Faker<Product>("en_US")
            .CustomInstantiator(f => new Product(
                name: GenerateRandomProductName(),
                description: GenerateRandomProductDescription(),
                price: GenerateRandomProductPrice(),
                image: GenerateRandomProductImage(),
                categoryId: Guid.NewGuid(),
                dimensions: DimensionsFixture.CreateValidDimensions(),
                active,
                stock: stock ?? GenerateRandomProductStock()
            )
        );

        return products.Generate(quantity);
    }

    public Product CreateInvalidProductWithoutName() =>
        CreateItemsWithAnInvalidProperty(nameof(Product.Name));

    public Product CreateInvalidProductWithoutDescription() =>
        CreateItemsWithAnInvalidProperty(nameof(Product.Description));

    public Product CreateInvalidProductWithZeroPrice() =>
        CreateItemsWithAnInvalidProperty(nameof(Product.Price));

    public Product CreateInvalidProductWithEmptyCategoryId() =>
        CreateItemsWithAnInvalidProperty(nameof(Product.CategoryId));

    public Product CreateInvalidProductWithNoImage() =>
        CreateItemsWithAnInvalidProperty(nameof(Product.Image));

    private Product CreateItemsWithAnInvalidProperty(string invalidPropertyName) =>
        new(
            name: invalidPropertyName == nameof(Product.Name) ? "" : GenerateRandomProductName(),
            description: invalidPropertyName == nameof(Product.Description) ? "" : GenerateRandomProductDescription(),
            price: invalidPropertyName == nameof(Product.Price) ? 0.0m : GenerateRandomProductPrice(),
            image: invalidPropertyName == nameof(Product.Image) ? "" : GenerateRandomProductImage(),
            categoryId: invalidPropertyName == nameof(Product.CategoryId) ? Guid.Empty : Guid.NewGuid(),
            stock: GenerateRandomProductStock(),
            dimensions: DimensionsFixture.CreateValidDimensions()
        );

    private string GenerateRandomProductName() =>
        _faker.Commerce.ProductName();

    private string GenerateRandomProductDescription() =>
        _faker.Commerce.ProductDescription();

    private decimal GenerateRandomProductPrice() =>
        _faker.Random.Decimal(1.0m, 1000.0m);

    private int GenerateRandomProductStock() =>
        _faker.Random.Number(10, 100);

    private string GenerateRandomProductImage() =>
        _faker.System.FilePath();

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
