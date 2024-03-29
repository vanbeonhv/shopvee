using AutoMapper;
using Microsoft.EntityFrameworkCore;
using shopveeAPI.Common;
using shopveeAPI.DbContext;
using shopveeAPI.Models;
using shopveeAPI.Repository;
using shopveeAPI.Services.Product.Dto.Request;
using shopveeAPI.Services.Product.Dto.Response;

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
            var entity = _mapper.Map<ProductEntity>(request);
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

    public async Task<ServiceResponse> GetProductDetail(Guid? id)
    {
        try
        {
            var product = await _shopveeDbContext.ProductEntity
                .Include(x => x.ProductOptionValues)
                .ThenInclude(x => x.ProductOption)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<ProductResponse>(product);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}