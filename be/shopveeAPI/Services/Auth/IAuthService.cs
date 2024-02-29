using shopveeAPI.Services.Auth.Dto.Request;
using shopveeAPI.Services.Auth.Dto.Response;

namespace shopveeAPI.Services.Auth;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest authRequest);
}