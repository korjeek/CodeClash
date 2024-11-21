using CodeClash.API.Services;
using CodeClash.Core.Models.Identity;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = CodeClash.Core.Models.Identity.LoginRequest;
using RegisterRequest = CodeClash.Core.Models.Identity.RegisterRequest;

namespace CodeClash.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController(AuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var user = await authService.CreateUser(request);
        if (user is null)
            return BadRequest(AuthRequestErrorType.ExistedAccount.ToString());
        
        return await Login(new LoginRequest(request.Email, request.Password));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await authService.GetUser(request);
        if (user is null)
            return BadRequest(AuthRequestErrorType.WrongCredentials.ToString());
        
        var tokens = await authService.UpdateUsersTokens(user);
        return Ok(new AuthResponse(user.Name, user.Email, tokens.AccessToken, tokens.RefreshToken));
    }
    
    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(JwtToken? tokenModel)
    {
        if (tokenModel is null)
            return BadRequest(AuthRequestErrorType.InvalidTokenModel.ToString());

        var user = await authService.GetUserByPrincipalClaims(tokenModel);
        if (user == null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return BadRequest(AuthRequestErrorType.ComplexRefreshTokenError);
        
        var tokens = authService.UpdateUsersTokens(user);
        return new ObjectResult(tokens);
    }
}

internal enum AuthRequestErrorType
{
    ExistedAccount,
    WrongCredentials,
    InvalidTokenModel,
    ComplexRefreshTokenError
}