using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using shopveeAPI.Common;
using shopveeAPI.Models;
using shopveeAPI.Services.ProductOptionValue.Dto.Request;
using shopveeAPI.UnitOfWork;

namespace shopveeAPI.Controllers;

[Route("api/product-option-value")]
[ApiController]
public class ProductOptionValueController: ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductOptionValueController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    [HttpPut("add")]
    public async Task<ServiceResponse> AddProductOptionValue([FromBody] ProductOptionValueRequest request)
    {
        return await _unitOfWork.ProductOptionValueService.Add(request);
    }
    
    [HttpPost("update")]
    public async Task<int> UpdateProductOptionValue([FromBody] ProductOptionValueRequest request)
    {
        var entity = _mapper.Map<ProductOptionValueEntity>(request);
        return await _unitOfWork.ProductOptionValueService.Update(entity);
    }
}