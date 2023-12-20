using Bogus;

namespace DevStore.Catalog.Domain.Tests.Fixtures;

public class DimensionsFixture : IDisposable
{
    private readonly Faker _faker = new("en_US");

    public Dimensions CreateValidDimensions()
    {
        var dimensions = new Faker<Dimensions>("en_US")
            .CustomInstantiator(f => new Dimensions(
                width: GenerateRandomDimensionValue(),
                height: GenerateRandomDimensionValue(),
                depth: GenerateRandomDimensionValue()
            )
        );

        return dimensions.Generate();
    }

    public Dimensions CreateInvalidDimensionsWithWidthtZero()
        => new(height: GenerateRandomDimensionValue(), width: 0, depth: GenerateRandomDimensionValue());

    public Dimensions CreateInvalidDimensionsWithHeightZero()
        => new(height: 0, width: GenerateRandomDimensionValue(), depth: GenerateRandomDimensionValue());

    public Dimensions CreateInvalidDimensionsWithDepthZero()
        => new(height: GenerateRandomDimensionValue(), width: GenerateRandomDimensionValue(), depth: 0);

    public int GenerateRandomDimensionValue() =>
        _faker.Random.Number(1, 100);

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
