namespace DevStore.Common.DomainObjects;

public record DomainValidationResult
{
    public required bool IsValid { get; set; }
    public required List<string> Messages { get; set; }
}
