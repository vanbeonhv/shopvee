using shopveeAPI.DbContext;
using shopveeAPI.Repository;

namespace shopveeAPI.Services.User;
using Models;
public class UserGenericServices: GenericRepository<User>, IUserGenericService
{
    public UserGenericServices(ShopveeDbContext shopveeDbContext) : base(shopveeDbContext)
    {
        
    }
}