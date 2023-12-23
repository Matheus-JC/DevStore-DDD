using AutoMapper;
using DevStore.Catalog.Application.DTOs;
using DevStore.Catalog.Domain;

namespace DevStore.Catalog.Application.Mappings;

public class DTOToDomainMappingProfile : Profile
{
    public DTOToDomainMappingProfile()
    {
        CreateMap<ProductDTO, Product>()
            .ConstructUsing(p =>
                new Product(p.Name, p.Description, p.Price, p.Image, p.CategoryId, 
                    new Dimensions(p.Height, p.Width, p.Depth), 
                    p.Active, p.Stock));

        CreateMap<CategoryDTO, Category>()
            .ConstructUsing(c => new Category(c.Name, c.Code));
    }
}
