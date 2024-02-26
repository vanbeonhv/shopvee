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
        var products = await _unitOfWork.ProductService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<ServiceResponse> GetProductDetail(Guid? id)
    {
        return await _unitOfWork.ProductService.GetProductDetail(id);
    }

    [HttpPost()]
    public async Task<ServiceResponse> AddProduct([FromBody] ProductRequest request)
    {
        return await _unitOfWork.ProductService.Add(request);
    }

    [HttpPut("{id}")]
    public async Task<ServiceResponse> UpdateProduct(Guid id, [FromBody] ProductRequest request)
    {
        return await _unitOfWork.ProductService.Update(id, request);
    }
}