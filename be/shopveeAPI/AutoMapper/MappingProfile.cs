using AutoMapper;
using shopveeAPI.Models;
using shopveeAPI.Services.Product.Dto.Request;
using shopveeAPI.Services.Product.Dto.Response;

namespace shopveeAPI.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductRequest, ProductEntity>();
        CreateMap<ProductEntity, ProductResponse>();
    }
}