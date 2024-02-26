using shopveeAPI.Common;
using shopveeAPI.Services.ProductOption.Dto.Request;

namespace shopveeAPI.Services.ProductOption;

public interface IProductOptionService
{
    Task<ServiceResponse> Add(ProductOptionRequest request);
}