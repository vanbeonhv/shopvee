using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;

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

        if (identity == null || !identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userClaims = identity.Claims;
        var idClaimValue = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(idClaimValue, out Guid parsedId))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var user = new User()
        {
            Id = parsedId,
            Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
            FullName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value
        };

        if (user.Email == null || user.FullName == null)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}