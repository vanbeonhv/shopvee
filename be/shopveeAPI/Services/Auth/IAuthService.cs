using shopveeAPI.Services.Auth.Dto.Response;
using shopveeAPI.Services.User.Dto.Request;

namespace shopveeAPI.Services.Auth;

public interface IAuthService
{
    Task<AuthResponse> Login(UserRequest userRequest);
}