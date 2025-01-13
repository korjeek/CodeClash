using CodeClash.Application.Services;
using CodeClash.Core.Models.Identity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = CodeClash.Core.Models.Identity.LoginRequest;
using RegisterRequest = CodeClash.Core.Models.Identity.RegisterRequest;

namespace CodeClash.API.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("api/auth")]
public class AuthenticationController(AuthService authService, TokenService tokenService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest("Incorrect request data.");
        var userResult = await authService.CreateUser(request.UserName, request.Email, request.Password);
        if (userResult.IsFailure)
            return BadRequest(userResult.Error);
        return await Login(new LoginRequest(request.Email, request.Password));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest("Incorrect request data.");
        var userResult = await authService.GetVerifiedUser(request.Email, request.Password);
        if (userResult.IsFailure)
            return BadRequest(userResult.Error);
        var user = userResult.Value;
        var tokens = await authService.UpdateUserTokens(user);
        var cookieOptions = new CookieOptions { SameSite = SameSiteMode.None };
        HttpContext.Response.Cookies.Append("spooky-cookies", tokens.AccessToken, cookieOptions);
        HttpContext.Response.Cookies.Append("olega-na-front", tokens.RefreshToken, cookieOptions);
        var authResponse = new AuthResponse(user.Name, user.Email, tokens.AccessToken, tokens.RefreshToken);
        return Ok(authResponse);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        if (!Request.Cookies.TryGetValue("spooky-cookies", out var accessToken))
            return BadRequest("User has not accessToken. To get it login");
        if (!Request.Cookies.TryGetValue("olega-na-front", out var refreshToken))
            return BadRequest("User has not refreshToken. To get it login or register.");

        var userResult = await authService.GetUserByPrincipalClaims(accessToken);
        if (userResult.IsFailure ||
            userResult.Value.RefreshToken != refreshToken ||
            userResult.Value.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return BadRequest($"ComplexRefreshTokenError, {userResult.Value.RefreshTokenExpiryTime}");

        var newAccessToken = tokenService.CreateAccessToken(userResult.Value);
        HttpContext.Response.Cookies.Append("spooky-cookies", newAccessToken, new CookieOptions {SameSite = SameSiteMode.None});
        return Ok(newAccessToken);
    }
}