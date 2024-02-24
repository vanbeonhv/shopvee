using shopveeAPI.Common;
using shopveeAPI.Models;
using shopveeAPI.Repository;
using shopveeAPI.Services.Product.Dto.Request;

namespace shopveeAPI.Services.Product;

public interface IProductService: IGenericRepository<Models.ProductEntity>
{
    Task<ServiceResponse> Add(ProductRequest request);
    Task<ServiceResponse> Update(Guid id, ProductRequest request);

}