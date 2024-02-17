using DevStore.Common.DomainObjects;

namespace DevStore.Catalog.Domain;

public class Product : Entity, IAggregateRoot
{
    public Guid CategoryId { get; private set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string Image { get; private set; } = null!;
    public bool Active { get; private set; }
    public DateTime CreationDate { get; private set; } = DateTime.UtcNow;

    public Category? Category { get; private set; } = null;
    public Dimensions Dimensions { get; private set; } = null!;

    protected Product() { }

    public Product(string name, string description, decimal price, string image, 
        Guid categoryId, Dimensions dimensions, bool active = true, int stock = 0)
    {
        AssertionConcern.AssertArgumentNotEmpty(name, 
            $"The '{nameof(Name)}' field cannot be empty");

        AssertionConcern.AssertArgumentNotEmpty(description, 
            $"The '{nameof(Description)}' field cannot be empty");

        AssertionConcern.AssertArgumentGreaterThanZero(price, 
            $"The '{nameof(Price)}' field must be greater than zero");

        AssertionConcern.AssertArgumentNotEmpty(image, 
            $"The '{nameof(Image)}' field cannot be empty");

        AssertionConcern.AssertArgumentNotEquals(categoryId, Guid.Empty, 
            $"The '{nameof(CategoryId)}' field cannot be empty");

        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Image = image;
        Active = active;
        CategoryId = categoryId;
        Dimensions = dimensions;
    }

    public void Activate() => Active = true;

    public void Deactivate() => Active = false;

    public void SetCategory(Category category)
    {
        Category = category;
        CategoryId = category.Id;
    }

    public void SetDescription(string description)
    {
        AssertionConcern.AssertArgumentNotEmpty(description, $"The '{nameof(Description)}' field cannot be empty");
        Description = description;
    }

    public void DebitStock(int quantity)
    {
        if (quantity < 0) 
            quantity *= -1;

        if (!HasQuantityInStock(quantity))
            throw new DomainException("The quantity in stock is insufficient");

        Stock -= quantity;
    }

    public bool HasQuantityInStock(int quantity)
    {
        return Stock >= quantity;
    }

    public void ReplenishStock(int quantity)
    {
        AssertionConcern.AssertArgumentIsPositive(quantity, "The quantity cannot be negative");

        Stock += quantity;
    }
}

