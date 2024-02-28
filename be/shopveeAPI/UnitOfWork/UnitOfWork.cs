using shopveeAPI.DbContext;
using shopveeAPI.Services.Address;
using shopveeAPI.Services.Auth;
using shopveeAPI.Services.Product;
using shopveeAPI.Services.User;

namespace shopveeAPI.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ShopveeDbContext _dbContext;

    public UnitOfWork(ShopveeDbContext dbContext, IUserGenericService userGenericService, IAuthService authService, IProductService productService)
    {
        _dbContext = dbContext;
        _userGenericService = userGenericService;
        _authService = authService;
        _productService = productService;
    }

    public IUserGenericService _userGenericService { get; set; }
    public IAuthService _authService { get; set; }
    public IProductService _productService { get; set; }
    public IAddressServices _addressresponse { get; set; }

    public int SaveChange()
    {
        return _dbContext.SaveChanges();
    }
}