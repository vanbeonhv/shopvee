using Microsoft.AspNetCore.Mvc;
using shopveeAPI.Services.User.Dto.Request;

namespace shopveeAPI.Services.Auth;
using Models;

public interface IAuthService
{
    Task<User?> Login(UserRequest userRequest);
    Task<IActionResult> UpdateRefreshToken(UserUpdateRefreshTokenRequest userUpdateRefreshTokenRequest);
}