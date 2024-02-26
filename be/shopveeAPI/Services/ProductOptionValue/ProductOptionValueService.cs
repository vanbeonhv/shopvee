using AutoMapper;
using shopveeAPI.Common;
using shopveeAPI.DbContext;
using shopveeAPI.Models;
using shopveeAPI.Repository;
using shopveeAPI.Services.ProductOptionValue.Dto.Request;
using shopveeAPI.Services.ProductOptionValue.Dto.Response;

namespace shopveeAPI.Services.ProductOptionValue;

public class ProductOptionValueService: GenericRepository<ProductOptionValueEntity>, IProductOptionValueService
{
    private readonly ShopveeDbContext _shopveeDbContext;
    private readonly IMapper _mapper;
    
    public ProductOptionValueService(ShopveeDbContext shopveeDbContext, IMapper mapper) : base(shopveeDbContext)
    {
        _shopveeDbContext = shopveeDbContext;
        _mapper = mapper;
    }
    
    public async Task<ServiceResponse>Add(ProductOptionValueRequest request)
    {
        try
        {
            var entity = _mapper.Map<ProductOptionValueEntity>(request);
            await _shopveeDbContext.ProductOptionValueEntity.AddAsync(entity);
            await _shopveeDbContext.SaveChangesAsync();
            var response = _mapper.Map<ProductOptionValuesResponse>(entity);
            return Created(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}