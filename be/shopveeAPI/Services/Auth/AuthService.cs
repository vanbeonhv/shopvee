using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using shopveeAPI.DbContext;
using shopveeAPI.Services.Auth.Dto.Response;
using shopveeAPI.Services.User.Dto.Request;
using shopveeAPI.Utils;

namespace shopveeAPI.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly ShopveeDbContext _shopveeDbContext;

    public AuthService(ShopveeDbContext shopveeDbContext, IConfiguration configuration)
    {
        _shopveeDbContext = shopveeDbContext;
        _configuration = configuration;
    }

    public async Task<AuthResponse> Login(UserRequest userRequest)
    {
        try
        {
            var authResponse = new AuthResponse();
            //Buoc 1: Login
            var user = await _shopveeDbContext.User.FirstOrDefaultAsync(u =>
                u.Email == userRequest.Email && u.Password == userRequest.Password);
            if (user == null)
            {
                authResponse.Message = "Invalid email or password";
                authResponse.ResponseCode = -1;
                return authResponse;
            }

            //Buoc 2: Tao token
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName ?? string.Empty),
            };

            var newAccessToken = CreateToken(authClaims);
            var token = new JwtSecurityTokenHandler().WriteToken(newAccessToken);
            var refreshToken = Helper.GenerateRefreshToken();

            //Buoc 3: update refreshToken vao db
            var expired = _configuration["JWT:RefreshTokenValidityInDays"] ?? String.Empty;
            await UpdateRefreshToken(new UserUpdateRefreshTokenRequest()
            {
                Id = user.Id,
                RefreshToken = refreshToken,
                RefreshTokenExpired = DateTime.Now.AddDays(int.Parse(expired))
            });

            authResponse.Token = token;
            authResponse.RefreshToken = refreshToken;
            authResponse.Message = "login successfully";
            authResponse.ResponseCode = 1;
            return authResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private JwtSecurityToken CreateToken(List<Claim> authClaims)
    {
        var authSignInKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET") ?? string.Empty));
        _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMunute);

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddMinutes(tokenValidityInMunute),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }


    private async Task<IActionResult> UpdateRefreshToken(UserUpdateRefreshTokenRequest userUpdateRefreshTokenRequest)
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