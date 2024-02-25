using Microsoft.AspNetCore.Mvc;
using shopveeAPI.Common;
using shopveeAPI.Services.Product;
using shopveeAPI.Services.Product.Dto.Request;
using shopveeAPI.UnitOfWork;

namespace shopveeAPI.Controllers;

[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    
    public ProductController(IUnitOfWork unitOfWork, IProductService productService)
    {
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet("get-all")]
    public async Task<ActionResult> GetAllProduct()
    {
        var products = await _unitOfWork._productService.GetAllAsync();
        return Ok(products);
    }
    
    [HttpPost()]
    public async Task<ServiceResponse> AddProduct([FromBody] ProductRequest request)
    {
        return await _unitOfWork._productService.Add(request);
    }
    
    [HttpPut("{id}")]
    public async Task<ServiceResponse> UpdateProduct(Guid id, [FromBody] ProductRequest request)
    {
        return await _unitOfWork._productService.Update(id, request);
    }
}
