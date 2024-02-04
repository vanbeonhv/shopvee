using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using shopveeAPI.Services.Auth.Dto.Response;
using shopveeAPI.Services.User.Dto.Request;
using shopveeAPI.UnitOfWork;
using shopveeAPI.Utils;

namespace shopveeAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IValidator<UserRequest> _validator;

        public AuthController(IUnitOfWork unitOfWork, IConfiguration configuration, IValidator<UserRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _validator = validator;
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(UserRequest userRequest)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(userRequest);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            var authResponse = new AuthResponse();
            try
            {
                //Buoc 1: Logic
                var userLogin = await _unitOfWork._authService.Login(userRequest);
                if (userLogin == null)
                {
                    authResponse.ResponseCode = -1;
                    authResponse.Message = "Invalid email or password";
                    return Ok(authResponse);
                }

                //Buoc 2: Tao token
                var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, userLogin.Id.ToString()),
                    new Claim(ClaimTypes.Email, userLogin.Email),
                    new Claim(ClaimTypes.Name, userLogin.FullName ?? string.Empty),
                };

                var newAccessToken = CreateToken(authClaims);
                var token = new JwtSecurityTokenHandler().WriteToken(newAccessToken);
                var refreshToken = Helper.GenerateRefreshToken();

                //Buoc 3: update refreshToken vao db
                var expired = _configuration["JWT:RefreshTokenValidityInDays"] ?? String.Empty;
                await _unitOfWork._authService.UpdateRefreshToken(new UserUpdateRefreshTokenRequest()
                {
                    Id = userLogin.Id,
                    RefreshToken = refreshToken,
                    RefreshTokenExpired = DateTime.Now.AddDays(int.Parse(expired))
                });

                authResponse.Token = token;
                authResponse.RefreshToken = refreshToken;
                authResponse.Message = "login successfully";
                authResponse.ResponseCode = 1;
                return Ok(authResponse);
            }
            catch (Exception e)
            {
                authResponse.ResponseCode = -1;
                authResponse.Message = e.Message;
                return Ok(authResponse);
            }
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET") ?? string.Empty));
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
    }
}