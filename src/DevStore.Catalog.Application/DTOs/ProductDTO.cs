using System.ComponentModel.DataAnnotations;

namespace DevStore.Catalog.Application.DTOs;

public class ProductDTO
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The {0} field is required")]
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "The {0} field is required")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The {0} field is required")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "The {0} field is required")]
    public bool Active { get; set; }

    [Required(ErrorMessage = "The {0} field is required")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "The {0} field is required")]
    public DateTime CreationDate { get; set; }

    [Required(ErrorMessage = "The {0} field is required")]
    public string? Image { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Field {0} must have a minimum value of {1}")]
    [Required(ErrorMessage = "The {0} field is required")]
    public int Stock { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Field {0} must have a minimum value of {1}")]
    [Required(ErrorMessage = "The {0} field is required")]
    public int Height { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Field {0} must have a minimum value of {1}")]
    [Required(ErrorMessage = "The {0} field is required")]
    public int Width { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Field {0} must have a minimum value of {1}")]
    [Required(ErrorMessage = "The {0} field is required")]
    public int Depth { get; set; }

    public IEnumerable<CategoryDTO>? Categories { get; set; }
}
