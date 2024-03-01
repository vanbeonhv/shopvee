using shopveeAPI.Common;
using shopveeAPI.Services.Address.Dto.Request;
using shopveeAPI.Services.Product.Dto.Request;

namespace shopveeAPI.Services.Address
{
    public interface IAddressServices
    {
        Task<ServiceResponse> Add(AddressRequest request);
        Task<ServiceResponse> Update(Guid id, AddressRequest request);
        Task<ServiceResponse> GetAllAddressAsync();
        Task<ServiceResponse> GetAddressAsync(Guid id);
        Task<ServiceResponse> DeleteAddressAsync(Guid id);
    }
}
