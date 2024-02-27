using shopveeAPI.Common;
using shopveeAPI.Repository;
using shopveeAPI.Services.PaymentMethod.Dto.Request;

namespace shopveeAPI.Services.PaymentMethod.Dto;

public interface IPaymentMethodService : IGenericRepository<Models.PaymentMethodEntity>
{
    public new Task<ServiceResponse> GetAllAsync();
    public new Task<ServiceResponse> GetByIdAsync(Guid id);
    public Task<ServiceResponse> Add(PaymentMethodCreateRequest request);
    public Task<ServiceResponse> Update(PaymentMethodUpdateRequest request);
    public new Task<ServiceResponse> Delete(Guid id);
}