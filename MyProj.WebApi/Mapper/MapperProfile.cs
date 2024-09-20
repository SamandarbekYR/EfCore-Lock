using AutoMapper;
using MyProj.WebApi.DTOs;
using MyProj.WebApi.Entities.Products;

namespace MyProj.WebApi.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, AddProductDto>().ReverseMap();
        }
    }
}
