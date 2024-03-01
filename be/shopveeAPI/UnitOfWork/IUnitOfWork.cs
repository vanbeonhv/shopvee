using shopveeAPI.Services.Address;
using shopveeAPI.Services.Auth;
using shopveeAPI.Services.Product;
using shopveeAPI.Services.User;
// ReSharper disable InconsistentNaming

namespace shopveeAPI.UnitOfWork;

public interface IUnitOfWork
{
    public IUserGenericService _userGenericService { get; set; }
    public IAuthService _authService { get; set; }
    public IProductService _productService { get; set; }
    public IAddressServices _addressServices{ get; set; }

    int SaveChange();
}