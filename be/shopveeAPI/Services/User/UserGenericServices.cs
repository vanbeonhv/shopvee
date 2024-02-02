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

    public async Task<IActionResult> Login(UserRequest userRequest)
    {
        try
        {
            var user = await _shopveeDbContext.User.FirstOrDefaultAsync(u =>
                u.Email == userRequest.Email && u.Password == userRequest.Password);
            if (user == null)
            {
                return new BadRequestObjectResult("Invalid username or password");
            }

            return new OkObjectResult("login success");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IActionResult> UpdateRefreshToken(UserUpdateRefreshTokenRequest userUpdateRefreshTokenRequest)
    {
        return new OkObjectResult("updated");
    }
}