using Microsoft.AspNetCore.Mvc;
using shopveeAPI.Services.Auth.Dto.Response;
using shopveeAPI.Services.User.Dto.Request;

namespace shopveeAPI.Services.Auth;
using Models;

public interface IAuthService
{
    Task<AuthResponse> Login(UserRequest userRequest);
}