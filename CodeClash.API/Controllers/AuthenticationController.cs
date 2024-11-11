using CodeClash.Application.Services;
using CodeClash.Core.Models.Identity;
using CodeClash.Core.Services;
using CodeClash.Persistence.Repositories;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RegisterRequest = CodeClash.Core.Models.Identity.RegisterRequest;

namespace CodeClash.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController(TokenService tokenService, UsersRepository usersRepository, UserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var user = await userService.CreateUser(request.UserName, request.Email);
        if (!user.Succeeded)
            return BadRequest();
        
        return await Login(new LoginRequest
        {
            Email = request.Email,
            Password = request.Password,
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var isLoginValid = await userService.IsLoginValid(request.Email, request.Password);
        if (!isLoginValid)
            return BadRequest("Login or password is incorrect");
        
        var user = await usersRepository.FindUserByEmail(request.Email);
        if (user is null)
            return Unauthorized();

        var tokens = tokenService.UpdateTokens(user);
        return Ok(new AuthResponse(user.UserName!, user.Email!, tokens.AccessToken, tokens.RefreshToken));
    }
    
    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(JwtToken? tokenModel)
    {
        if (tokenModel is null)
            return BadRequest();
        
        var principal = tokenService.GetPrincipal(tokenModel.AccessToken);
        if (principal == null)
            return BadRequest();

        var user = await userService.FindUserByUsername(principal.Identity!.Name);
        if (user == null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return BadRequest();
        
        var tokens = tokenService.CreateTokensByPrincipleClaims(principal.Claims.ToList());
        userService.UpdateRefreshToken(user, tokens.RefreshToken);

        return new ObjectResult(tokens);
    }
}