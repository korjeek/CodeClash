using CodeClash.API.Services;
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
            return BadRequest(ModelState);
        var userResult = await authService.CreateUser(request.UserName, request.Email, request.Password);
        if (userResult.IsFailure)
            return BadRequest(userResult.Error);
        
        return await Login(new LoginRequest(request.Email, request.Password));
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var userResult = await authService.GetUser(request.Email, request.Password);
        if (userResult.IsFailure)
            return BadRequest(userResult.Error);
        var user = userResult.Value;
        var tokens = await authService.UpdateUsersTokens(user);
        HttpContext.Response.Cookies.Append("spooky-cookies", tokens.AccessToken);
        
        return Ok(new AuthResponse(user.Name, user.Email, tokens.AccessToken, tokens.RefreshToken));
    }
    
    [HttpPut] // TODO: Может изменить на PUT? Обычно именно его используют для обновления данных на сервере, а использовать везде только POST это КОЛХОЗ, ну и плохо в целом, так Ваня сказал
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(JwtToken? tokenModel)
    {
        if (tokenModel is null)
            return BadRequest(AuthRequestErrorType.InvalidTokenModel.ToString());

        var userResult = await authService.GetUserByPrincipalClaims(tokenModel);
        if (userResult.IsFailure || 
            userResult.Value.RefreshToken != tokenModel.RefreshToken || 
            userResult.Value.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return BadRequest(AuthRequestErrorType.ComplexRefreshTokenError);
        
        var tokens = await authService.UpdateUsersTokens(userResult.Value);
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