using Microsoft.AspNetCore.Authentication;

namespace shopveeAPI.Middleware;

public class AccessTokenMiddleware
{
    private readonly RequestDelegate _next;

    public AccessTokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var accessToken = await httpContext.GetTokenAsync("access_token");
        httpContext.Items["access_token"] = accessToken;
        await _next(httpContext);
    }
}