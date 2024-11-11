using CodeClash.Application.Services;
using CodeClash.Core.Models;
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
        
        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email
        };
        
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
        
        var user = usersRepository.FindUserByEmail(request.Email);
        if (user is null)
            return Unauthorized();
        
        var accessToken = tokenService.CreateToken(user);
        var refreshToken = tokenService.UpdateRefreshToken(user);
        
        return Ok(new AuthResponse(user.UserName!, user.Email!, accessToken, refreshToken));
    }
    
    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(JwtToken? tokenModel)
    {
        if (tokenModel is null)
            return BadRequest("Invalid client request");
        
        var refreshToken = tokenModel.RefreshToken;
        var principal = _configuration.GetPrincipalFromExpiredToken(tokenModel.AccessToken);
        
        if (principal == null)
        {
            return BadRequest("Invalid access token or refresh token");
        }
        
        var username = principal.Identity!.Name;
        var user = await _userManager.FindByNameAsync(username!);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        var newAccessToken = _configuration.CreateToken(principal.Claims.ToList());
        var newRefreshToken = _configuration.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }
}