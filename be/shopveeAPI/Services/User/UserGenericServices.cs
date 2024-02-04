using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopveeAPI.DbContext;
using shopveeAPI.Repository;
using shopveeAPI.Services.User.Dto.Request;

namespace shopveeAPI.Services.User;

using Models;

public class UserGenericServices : GenericRepository<User>, IUserGenericService
{
    private readonly ShopveeDbContext _shopveeDbContext;

    public UserGenericServices(ShopveeDbContext shopveeDbContext) : base(shopveeDbContext)
    {
        _shopveeDbContext = shopveeDbContext;
    }
    
}