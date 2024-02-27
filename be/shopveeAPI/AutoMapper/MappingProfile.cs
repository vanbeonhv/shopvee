using AutoMapper;
using shopveeAPI.Models;
using shopveeAPI.Services.PaymentMethod.Dto.Request;
using shopveeAPI.Services.PaymentMethod.Dto.Response;
using shopveeAPI.Services.Product.Dto.Request;
using shopveeAPI.Services.Product.Dto.Response;

namespace shopveeAPI.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductRequest, ProductEntity>();
        CreateMap<ProductEntity, ProductResponse>();
        CreateMap<PaymentMethodCreateRequest, PaymentMethodEntity>();
        CreateMap<PaymentMethodEntity, PaymentMethodReponse>();
    }
}