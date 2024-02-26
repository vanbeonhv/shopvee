using AutoMapper;
using shopveeAPI.Models;
using shopveeAPI.Services.Address.Dto.Request;
using shopveeAPI.Services.Address.Dto.Response;
using shopveeAPI.Services.Product.Dto.Request;
using shopveeAPI.Services.Product.Dto.Response;
using shopveeAPI.Services.ProductOption.Dto.Response;
using shopveeAPI.Services.ProductOptionValue.Dto.Request;
using shopveeAPI.Services.ProductOptionValue.Dto.Response;

namespace shopveeAPI.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductRequest, ProductEntity>();
        CreateMap<ProductEntity, ProductResponse>();
        CreateMap<AddressRequest, AddressEntity>();
        CreateMap<AddressEntity, AddressResponse>();
        CreateMap<ProductEntity, ProductResponse>().ForMember(dest => dest.ProductOptionValues,
            opt => opt.MapFrom(src => src.ProductOptionValues));
        CreateMap<ProductOptionEntity, ProductOptionResponse>();
        CreateMap<ProductOptionValueRequest, ProductOptionValueEntity>();
        CreateMap<ProductOptionValueEntity, ProductOptionValuesResponse>().ForMember(dest => dest.OptionName,
            opt => opt.MapFrom(src => src.ProductOption.OptionName));
    }
}