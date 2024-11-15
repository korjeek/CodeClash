using Microsoft.AspNetCore.Mvc;

namespace CodeClash.API.Endpoints;

//public record RegisterRequest(string Username, string Password, string Email);

//public record LoginRequest(string Email, string Password);

/*public static class AuthEndpoints
{
    private static readonly IConfiguration _configuration;
    
    public static void MapUserEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("register", Register);
        endpoints.MapPost("login", Login);
        endpoints.MapPost("refresh-token", RefreshToken);
    }

    private static async Task<IResult> Register(RegisterRequest request, UserService userService)
    {
        await userService.Register(request.Username, request.Email, request.Password);
        return Results.Ok();
    }

    private static async Task<IResult> Login(LoginRequest request, UserService userService, HttpContext httpContext)
    {
        var tokens = await userService.Login(request.Email, request.Password);
        if (tokens is null)
            return Results.Unauthorized();
        
        httpContext.Response.Cookies.Append("spooky-secret", tokens.AccessToken);
        httpContext.Response.Cookies.Append("scary-secret", tokens.RefreshToken);
        
        return Results.Ok(tokens);
    }

    private static async Task<IResult> RefreshToken(JwtTokens? tokens)
    {
        if (tokens is null)
            return Results.BadRequest("Invalid client request");
        
        var refreshToken = tokens.RefreshToken;
        var accessToken = tokens.AccessToken;
        var principle = _;
        
        if (principle is null)
            return Results.Unauthorized();
    }
}*/
