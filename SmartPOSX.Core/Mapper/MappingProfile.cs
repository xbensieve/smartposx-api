using AutoMapper;
using SmartPOSX.Core.DTOs.Categories;
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
        }
    }
}
