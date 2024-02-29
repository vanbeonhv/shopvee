using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using shopveeAPI.Common;
using shopveeAPI.Services.Address;
using shopveeAPI.Services.Address.Dto.Request;
using shopveeAPI.Services.Product.Dto.Request;
using shopveeAPI.UnitOfWork;


namespace shopveeAPI.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddressController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet()]
        public async Task<ServiceResponse> GetAllAddressAsync()
        {
            var listAddress = await _unitOfWork._addressresponse.GetAllAddressAsync();
            return listAddress;
        }
        [HttpGet("{id}")]
        public async Task<ServiceResponse> GetAddressAsync(Guid id)
        {
            var address = await _unitOfWork._addressresponse.GetAddressAsync(id);
            return address;
        }
        [HttpPost()]
        public async Task<ServiceResponse> Add(AddressRequest request)
        {
            return await _unitOfWork._addressresponse.Add(request);
        }
        [HttpPut("{id}")]
        public async Task<ServiceResponse> Update(Guid id,AddressRequest request)
        {
            return await _unitOfWork._addressresponse.Update(id, request);
        }
        //DeleteAddressAsync
        [HttpDelete("{id}")]
        public async Task<ServiceResponse> DeleteAddressAsync(Guid id)
        {
            return await _unitOfWork._addressresponse.DeleteAddressAsync(id);
        }
    }
}
