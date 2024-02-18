namespace shopveeAPI.Services.Auth.Dto.Response;

public class AuthResponse
{
    public int ResponseCode { get; set; }
    public string Message { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}