using AutoMapper;
using shopveeAPI.Common;
using shopveeAPI.DbContext;
using shopveeAPI.Models;
using shopveeAPI.Repository;
using shopveeAPI.Services.Product.Dto.Request;

namespace shopveeAPI.Services.Product;

public class ProductService : GenericRepository<ProductEntity>, IProductService
{
    private readonly ShopveeDbContext _shopveeDbContext;
    private readonly IMapper _mapper;

    public ProductService(ShopveeDbContext shopveeDbContext, IMapper mapper) : base(shopveeDbContext)
    {
        _shopveeDbContext = shopveeDbContext;
        _mapper = mapper;
    }

    public async Task<ServiceResponse> Add(ProductRequest request)
    {
        try
        {
            var entity = new ProductEntity
            {
                Name = request.Name,
                Description = request.Description,
                CategoryId = request.CategoryId,
                Image = request.Image,
                ShopId = request.ShopId,
            };
            await _shopveeDbContext.ProductEntity.AddAsync(entity);
            await _shopveeDbContext.SaveChangesAsync();
            return Created(entity);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    public async Task<ServiceResponse> Update(Guid id, ProductRequest request)
    {
        try
        {
            var entity = await _shopveeDbContext.ProductEntity.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            
            _mapper.Map(request, entity);
            
            _shopveeDbContext.ProductEntity.Update(entity);
            await _shopveeDbContext.SaveChangesAsync();
            return Ok(entity);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}