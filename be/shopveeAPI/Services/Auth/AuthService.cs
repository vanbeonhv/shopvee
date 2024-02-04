using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopveeAPI.DbContext;
using shopveeAPI.Services.User.Dto.Request;

namespace shopveeAPI.Services.Auth;

using Models;

public class AuthService : IAuthService
{
    private readonly ShopveeDbContext _shopveeDbContext;

    public AuthService(ShopveeDbContext shopveeDbContext)
    {
        _shopveeDbContext = shopveeDbContext;
    }

    public async Task<User?> Login(UserRequest userRequest)
    {
        try
        {
            return await _shopveeDbContext.User.FirstOrDefaultAsync(u =>
                u.Email == userRequest.Email && u.Password == userRequest.Password);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IActionResult> UpdateRefreshToken(UserUpdateRefreshTokenRequest userUpdateRefreshTokenRequest)
    {
        var user = await _shopveeDbContext.User.FirstOrDefaultAsync(u => u.Id == userUpdateRefreshTokenRequest.Id);
        if (user == null)
        {
            return new BadRequestObjectResult("User Id not found");
        }

        user.RefreshToken = userUpdateRefreshTokenRequest.RefreshToken;
        user.RefreshTokenExpired = userUpdateRefreshTokenRequest.RefreshTokenExpired;
        _shopveeDbContext.User.Update(user);
        await _shopveeDbContext.SaveChangesAsync();
        return new OkObjectResult("updated");
    }
}