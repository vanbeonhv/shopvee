using shopveeAPI.Services.User;
using shopveeAPI.DbContext;
using shopveeAPI.Services.Auth;

namespace shopveeAPI.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ShopveeDbContext _dbContext;
    public IUserGenericService _userGenericService { get; set; }
    public IAuthService _authService { get; set; }

    public UnitOfWork(ShopveeDbContext dbContext, IUserGenericService userGenericService, IAuthService authService)
    {
        _dbContext = dbContext;
        _userGenericService = userGenericService;
        _authService = authService;
    }


    public int SaveChange()
    {
        return _dbContext.SaveChanges();
    }
}