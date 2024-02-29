namespace shopveeAPI.Services.Auth.Dto.Request;

public class AuthRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}