﻿using DevStore.Common.DomainObjects;

namespace DevStore.Catalog.Domain;

public class Category : Entity
{
    public string Name { get; private set; } = null!;
    public int Code { get; private set; }
    public DateTime CreationDate { get; private set; } = DateTime.UtcNow;

    public readonly List<Product> _products = [];
    public IReadOnlyCollection<Product> Products => _products;

    protected Category(){}

    public Category(string name, int code)
    {
        AssertionConcern.AssertArgumentNotEmpty(name, 
            $"The '{nameof(Name)}' field cannot be empty");

        AssertionConcern.AssertArgumentGreaterThanZero(code, 
            $"The '{nameof(Code)}' field must be greater than zero");

        Name = name;
        Code = code;
    }

    public override string ToString() => $"{Name} - {Code}";
}
