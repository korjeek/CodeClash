using CodeClash.Application.Services;
using CodeClash.Core.Models;
using CodeClash.Core.Models.Identity;
using CodeClash.Core.Services;
using CodeClash.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = CodeClash.Core.Models.Identity.LoginRequest;
using RegisterRequest = CodeClash.Core.Models.Identity.RegisterRequest;

namespace CodeClash.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController(TokenService tokenService, UsersRepository usersRepository, AuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var password = await authService.HashPassword(request.Password);
        var user = await usersRepository.AddUser(new User(request.UserName, request.Email, password));
        if (user is null)
            return BadRequest("Seems like this user is already registered");
        
        return await Login(new LoginRequest(request.Email, request.Password));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var isLoginValid = await authService.IsLoginValid(request.Email, request.Password);
        if (!isLoginValid)
            return BadRequest("Login or password is incorrect");
        
        var user = await usersRepository.FindUserByEmail(request.Email);
        if (user is null)
            return Unauthorized("Not logged in");

        var tokens = tokenService.UpdateTokens(user);
        return Ok(new AuthResponse(user.UserName, user.Email, tokens.AccessToken, tokens.RefreshToken));
    }
    
    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(JwtToken? tokenModel)
    {
        if (tokenModel is null)
            return BadRequest("Invalid token model");
        
        var principal = tokenService.GetPrincipalClaims(tokenModel.AccessToken);
        if (principal == null)
            return BadRequest("Invalid principal claims");

        var user = await usersRepository.FindUserByUserName(principal.Identity!.Name);
        if (user == null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return BadRequest();
        
        var tokens = tokenService.CreateTokensByPrincipleClaims(principal.Claims.ToList());
        usersRepository.UpdateUsersRefreshToken(user.Id, tokens.RefreshToken);

        return new ObjectResult(tokens);
    }
}