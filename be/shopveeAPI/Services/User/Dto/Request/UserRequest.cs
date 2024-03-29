using shopveeAPI.Services.Auth.Dto.Request;

namespace shopveeAPI.Services.User.Dto.Request;

public class UserRequest: AuthRequest
{
    public string FullName { get; set; } = null!;
}

public class UserUpdateRefreshTokenRequest
{
    public Guid Id { get; set; }
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpired { get; set; }
}