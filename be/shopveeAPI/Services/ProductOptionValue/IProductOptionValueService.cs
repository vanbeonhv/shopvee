using shopveeAPI.Common;
using shopveeAPI.Models;
using shopveeAPI.Repository;
using shopveeAPI.Services.ProductOptionValue.Dto.Request;

namespace shopveeAPI.Services.ProductOptionValue;

public interface IProductOptionValueService : IGenericRepository<ProductOptionValueEntity>
{
    Task<ServiceResponse> Add(ProductOptionValueRequest request);
}