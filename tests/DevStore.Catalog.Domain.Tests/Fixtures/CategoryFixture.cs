using Bogus;

namespace DevStore.Catalog.Domain.Tests.Fixtures;

public class CategoryFixture : IDisposable
{
    private readonly Faker _faker = new("en_US");

    public Category CreateValidCategory()
    {
        var category = new Faker<Category>("en_US")
            .CustomInstantiator(f => new Category(
                name: GenerateRandomCategoryName(),
                code: GenerateRandomCategoryCode()
            )
        );

        return category.Generate();
    }

    public Category CreateInvalidCategoryWithEmptyName() =>
        new("", GenerateRandomCategoryCode());

    public Category CreateInvalidCategoryWithZeroCode() =>
        new(GenerateRandomCategoryName(), 0);

    private string GenerateRandomCategoryName() =>
        _faker.Commerce.Categories(1)?.ToString() ?? "Foo";

    private int GenerateRandomCategoryCode() =>
        _faker.Random.Number(1, 100);

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
