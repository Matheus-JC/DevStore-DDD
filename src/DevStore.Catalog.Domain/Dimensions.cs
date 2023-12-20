using DevStore.Common.DomainObjects;

namespace DevStore.Catalog.Domain;

public class Dimensions : ValueObject
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int Depth { get; set; }

    public Dimensions(int width, int height , int depth)
    {
        AssertionConcern.AssertArgumentGreaterThanZero(width, $"The '{nameof(Width)}' field must be greater than zero");
        AssertionConcern.AssertArgumentGreaterThanZero(height, $"The '{nameof(Height)}' field must be greater than zero");
        AssertionConcern.AssertArgumentGreaterThanZero(depth, $"The '{nameof(Depth)}' field must be greater than zero");

        Width = width; 
        Height = height; 
        Depth = depth;
    }

    public override string ToString()
    {
        return $"WxHxD: {Width} x {Height} x {Depth}";
    }
}