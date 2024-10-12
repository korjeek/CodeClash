using CodeClash.Application.Services;

namespace CodeClash.API.Endpoints;

public record RegisterRequest(string Username, string Password, string Email);

public record LoginRequest(string Email, string Password);

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("register", Register);
        endpoints.MapPost("login", Login);
        return endpoints;
    }

    private static async Task<IResult> Register(RegisterRequest request, UserService userService)
    {
        await userService.Register(request.Username, request.Email, request.Password);
        return Results.Ok();
    }

    private static async Task<IResult> Login(LoginRequest request, UserService userService)
    {
        var token = await userService.Login(request.Email, request.Password);
        return Results.Ok(token);
    }
}