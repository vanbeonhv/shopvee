using shopveeAPI.Services.User;
using shopveeAPI.DbContext;

namespace shopveeAPI.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ShopveeDbContext _dbContext;
    public IUserGenericService _userGenericService { get; set; }

    public UnitOfWork(ShopveeDbContext dbContext, IUserGenericService userGenericService)
    {
        _dbContext = dbContext;
        this._userGenericService = userGenericService;
    }


    public int SaveChange()
    {
        return _dbContext.SaveChanges();
    }
}