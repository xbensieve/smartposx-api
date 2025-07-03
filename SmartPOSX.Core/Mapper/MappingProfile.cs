using AutoMapper;
using SmartPOSX.Core.DTOs.Categories;
using SmartPOSX.Core.DTOs.Products;
using SmartPOSX.Core.DTOs.Roles;
using SmartPOSX.Domain.Entities;

namespace SmartPOSX.Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Role, RoleMapperModel>();
            CreateMap<Category, CategoryDto>();
            CreateMap<ProductVariation, ProductVariationDto>();
            CreateMap<VariationAttribute, VariationAttributeDto>();
            CreateMap<VariationImage, VariationImageDto>();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));
        }
    }
}
