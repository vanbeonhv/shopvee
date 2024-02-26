using AutoMapper;
using shopveeAPI.Common;
using shopveeAPI.DbContext;
using shopveeAPI.Models;
using shopveeAPI.Repository;
using shopveeAPI.Services.ProductOption.Dto;
using shopveeAPI.Services.ProductOption.Dto.Request;
using shopveeAPI.Services.ProductOption.Dto.Response;

namespace shopveeAPI.Services.ProductOption;

public class ProductOptionService : GenericRepository<ProductOptionEntity>, IProductOptionService
{
    private readonly ShopveeDbContext _shopveeDbContext;
    private readonly IMapper _mapper;

    public ProductOptionService(ShopveeDbContext shopveeDbContext, IMapper mapper) : base(shopveeDbContext)
    {
        _shopveeDbContext = shopveeDbContext;
        _mapper = mapper;
    }

    public async Task<ServiceResponse> Add(ProductOptionRequest request)
    {
        try
        {
            var entity = new ProductOptionEntity()
            {
                OptionName = request.OptionName
            };

            await _shopveeDbContext.ProductOptionEntity.AddAsync(entity);
            await _shopveeDbContext.SaveChangesAsync();
            var response = _mapper.Map<ProductOptionResponse>(entity);
            return Created(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}