using Microsoft.AspNetCore.Mvc;
using shopveeAPI.Common;
using shopveeAPI.Services.ProductOption.Dto.Request;
using shopveeAPI.UnitOfWork;

namespace shopveeAPI.Controllers;

[Route("api/product-option")]
[ApiController]
public class ProductOptionController: ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    
    public ProductOptionController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    [HttpPut("add")]
    public async Task<ServiceResponse> AddProductOption([FromBody] ProductOptionRequest request)
    {
        return await _unitOfWork.ProductOptionService.Add(request);
    }
}