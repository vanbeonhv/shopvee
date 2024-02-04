using shopveeAPI.DbContext;
using shopveeAPI.Services.Auth;
using shopveeAPI.Services.User;

namespace shopveeAPI.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ShopveeDbContext _dbContext;

    public UnitOfWork(ShopveeDbContext dbContext, IUserGenericService userGenericService, IAuthService authService)
    {
        _dbContext = dbContext;
        _userGenericService = userGenericService;
        _authService = authService;
    }

    public IUserGenericService _userGenericService { get; set; }
    public IAuthService _authService { get; set; }


    public int SaveChange()
    {
        return _dbContext.SaveChanges();
    }
}