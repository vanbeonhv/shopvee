namespace shopveeAPI.Services.User.Dto.Request;

public class UserRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FullName { get; set; } = null!;
}

public class UserUpdateRefreshTokenRequest
{
    public Guid Id { get; set; }
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpired { get; set; }
}