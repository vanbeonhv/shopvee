using shopveeAPI.DbContext;
using shopveeAPI.Services.Address;
using shopveeAPI.Services.Auth;
using shopveeAPI.Services.Product;
using shopveeAPI.Services.ProductOption;
using shopveeAPI.Services.ProductOptionValue;
using shopveeAPI.Services.User;

namespace shopveeAPI.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ShopveeDbContext _dbContext;

    public IUserGenericService UserGenericService { get; set; }
    public IAuthService AuthService { get; set; }
    public IProductService ProductService { get; set; }
    public IProductOptionService ProductOptionService { get; set; }
    public IProductOptionValueService ProductOptionValueService { get; set; }
    public IAddressServices _addressServices { get; set; }


    public UnitOfWork(ShopveeDbContext dbContext, IUserGenericService userGenericService, IAuthService authService,
        IProductService productService, IProductOptionService
            productOptionService, IProductOptionValueService productOptionValueService)
    {
        _dbContext = dbContext;
        UserGenericService = userGenericService;
        AuthService = authService;
        ProductService = productService;
        ProductOptionService = productOptionService;
        ProductOptionValueService = productOptionValueService;
    }

    public int SaveChange()
    {
        return _dbContext.SaveChanges();
    }
}