using shopveeAPI.DbContext;
using shopveeAPI.Repository;

namespace shopveeAPI.Services.User;

public class UserGenericServices : GenericRepository<Models.User>, IUserGenericService
{
    // ReSharper disable once NotAccessedField.Local
    private readonly ShopveeDbContext _shopveeDbContext;

    public UserGenericServices(ShopveeDbContext shopveeDbContext) : base(shopveeDbContext)
    {
        _shopveeDbContext = shopveeDbContext;
    }
}