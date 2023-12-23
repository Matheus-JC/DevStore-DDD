using System.ComponentModel.DataAnnotations;

namespace DevStore.Catalog.Application.DTOs;

public class CategoryDTO
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The {0} field is required")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "The {0} field is required")]
    public required int Code { get; set; }
}
