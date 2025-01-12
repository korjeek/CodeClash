using CodeClash.Application.Services;
using CodeClash.Core.Models.Identity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = CodeClash.Core.Models.Identity.LoginRequest;
using RegisterRequest = CodeClash.Core.Models.Identity.RegisterRequest;

namespace CodeClash.API.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("auth")]
public class AuthenticationController(AuthService authService) : ControllerBase
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
        HttpContext.Response.Cookies.Append("spooky-cookies", tokens.AccessToken);
        HttpContext.Response.Cookies.Append("olega-na-front", tokens.RefreshToken);
        var authResponse = new AuthResponse(user.Name, user.Email, tokens.AccessToken, tokens.RefreshToken);
        return Ok(authResponse);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        if (!Request.Cookies.TryGetValue("spooky-cookies", out var accessToken) || !Request.Cookies.TryGetValue("olega-na-front", out var refreshToken))
            return BadRequest("User has not essential cookies. To get it login or register.");
        var tokenModel = new JwtToken(accessToken, refreshToken);
        var userResult = await authService.GetUserByPrincipalClaims(tokenModel);
        if (userResult.IsFailure ||
            userResult.Value.RefreshToken != tokenModel.RefreshToken ||
            userResult.Value.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return BadRequest($"ComplexRefreshTokenError, {userResult.Value.RefreshTokenExpiryTime}");
        
        var tokens = await authService.UpdateUserTokens(userResult.Value);
        HttpContext.Response.Cookies.Append("spooky-cookies", tokens.AccessToken);
        HttpContext.Response.Cookies.Append("olega-na-front", tokens.RefreshToken);
        return Ok(tokens);
    }
}