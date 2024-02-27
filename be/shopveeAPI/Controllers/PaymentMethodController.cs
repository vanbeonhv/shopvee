using Microsoft.AspNetCore.Mvc;
using shopveeAPI.Common;
using shopveeAPI.Services.PaymentMethod.Dto.Request;
using shopveeAPI.UnitOfWork;

namespace shopveeAPI.Controllers;
[Route("api/product")]
[ApiController]
public class PaymentMethodController: ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public PaymentMethodController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    [HttpGet("get-all")]
    public async Task<ServiceResponse> GetAll()
    {
        return await _unitOfWork.PaymentMethodService.GetAllAsync();
    }
    [HttpGet("{id}")]
    public async Task<ServiceResponse> GetOne(Guid id)
    {
        return  await _unitOfWork.PaymentMethodService.GetByIdAsync(id);
    }
    [HttpPost()]
    public async Task<ServiceResponse> Add ([FromBody] PaymentMethodCreateRequest request)
    {
        return await _unitOfWork.PaymentMethodService.Add(request);
    }
    [HttpPut()]
    public async Task<ServiceResponse> Update([FromBody] PaymentMethodUpdateRequest request)
    {
        return await _unitOfWork.PaymentMethodService.Update(request);
    }
    [HttpDelete("{id}")]
    public async Task<ServiceResponse> Delete(Guid id)
    {
        return await _unitOfWork.PaymentMethodService.Delete(id);
    }
}