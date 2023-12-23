using AutoMapper;
using DevStore.Catalog.Application.DTOs;
using DevStore.Catalog.Domain;

namespace DevStore.Catalog.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Product, ProductDTO>()
            .ForMember(d => d.Width, o => o.MapFrom(s => s.Dimensions.Width))
            .ForMember(d => d.Height, o => o.MapFrom(s => s.Dimensions.Height))
            .ForMember(d => d.Depth, o => o.MapFrom(s => s.Dimensions.Depth));

        CreateMap<Category, CategoryDTO>();
    }
}
