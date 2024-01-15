using shopveeAPI.Services.User;
using shopveeAPI.DbContext;

namespace shopveeAPI.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ShopveeDbContext _dbContext;

    public UnitOfWork(ShopveeDbContext dbContext, IUserServices userService)
    {
        _dbContext = dbContext;
        this.userServices = userService;
    }

    public IUserServices userServices { get; set; }

    public int SaveChange()
    {
        return _dbContext.SaveChanges();
    }
}