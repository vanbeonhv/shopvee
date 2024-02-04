namespace shopveeAPI.Services.Auth.Dto.Response;

public class AuthResponse
{
    public int ResponseCode { get; set; }
    public string Message { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}