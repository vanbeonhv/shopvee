using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using shopveeAPI.DbContext;
using shopveeAPI.Responses;
using shopveeAPI.Services.Auth;

// ReSharper disable PossibleMultipleEnumeration

namespace shopveeAPI.Filter;

public class AuthorizeAttribute : TypeFilterAttribute
{
    public AuthorizeAttribute() : base(typeof(AuthorizeActionFilter))
    {
    }
}

public class AuthorizeActionFilter : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var identity = context.HttpContext.User.Identity as ClaimsIdentity;
        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

        if (identity == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!CheckTokenInfo(identity))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var response = await CheckTokenExpiry(context);

        if (response.StatusCode != (int)HttpStatusCode.Accepted)
        {
            context.Result = new JsonResult(response);
        }
    }


    private bool CheckTokenInfo(ClaimsIdentity identity)
    {
        var userClaims = identity.Claims;
        var idClaimValue = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(idClaimValue, out Guid parsedId))
        {
            return false;
        }

        var value = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (value != null)
        {
            var user = new User()
            {
                Id = parsedId,
                Email = value,
                FullName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value
            };

            if (user.FullName == null)
            {
                return false;
            }
        }

        return true;
    }

    private async Task<ApiResponse> CheckTokenExpiry(AuthorizationFilterContext context)
    {
        //Buoc 1: Lay token tu request
        var accessToken = context.HttpContext.Items["access_token"]?.ToString();
        if (accessToken == null)
        {
            return await Task.FromResult(new ApiResponse("invalid access token", (int)HttpStatusCode.Unauthorized));
        }

        var jwtSecurityToken = new JwtSecurityToken(accessToken);
        var validTo = jwtSecurityToken.ValidTo.ToLocalTime();

        if (validTo > DateTime.Now)
        {
            return await Task.FromResult(new ApiResponse("accepted token", (int)HttpStatusCode.Accepted));
        }

        //Buoc 2: Giai ma token dua vao secretKey da config truoc do
        var principal = GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
        {
            return await Task.FromResult(new ApiResponse("accepted token", (int)HttpStatusCode.Accepted));
        }

        //Buoc 3: Lay userName tu token ra 
        var userEmail = principal.Claims.ToList()[1].ToString().Split(" ")[1];

        //Buoc 4: Kiem tra RefreshToken het han chua
        var dbContext = context.HttpContext.RequestServices.GetRequiredService<ShopveeDbContext>();
        var user = await dbContext.User.FirstOrDefaultAsync(u => u.Email == userEmail);
        if (user == null || user.RefreshTokenExpired <= DateTime.Now)
        {
            return await Task.FromResult(new ApiResponse("expired refresh token", (int)HttpStatusCode.Unauthorized));
        }

        //Buoc 5: Khoi tai AccessToken khac va thay the token cu
        var authService = context.HttpContext.RequestServices.GetRequiredService<AuthService>();
        var newAccessToken = authService.CreateAccessToken(user);
        context.HttpContext.Request.Headers["Authorization"] = "Bearer" + newAccessToken;
        return await Task.FromResult(new ApiResponse("accepted token", (int)HttpStatusCode.Accepted));
    }


    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET") ?? String.Empty)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}