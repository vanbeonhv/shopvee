using shopveeAPI.Services.Address;
using shopveeAPI.Services.Auth;
using shopveeAPI.Services.Product;
using shopveeAPI.Services.ProductOption;
using shopveeAPI.Services.ProductOptionValue;
using shopveeAPI.Services.User;
// ReSharper disable InconsistentNaming

namespace shopveeAPI.UnitOfWork;

public interface IUnitOfWork
{
    public IUserGenericService UserGenericService { get; set; }
    public IAuthService AuthService { get; set; }
    public IProductService ProductService { get; set; }
    public IProductOptionService ProductOptionService { get; set; }
    public IProductOptionValueService ProductOptionValueService { get; set; }
    public IAddressServices _addressServices{ get; set; }

    int SaveChange();
}