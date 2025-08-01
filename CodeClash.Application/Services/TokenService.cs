﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CodeClash.Application.Extensions;
using CodeClash.Core.Models;
using CodeClash.Core.Models.Identity;
using Microsoft.Extensions.Configuration;

namespace CodeClash.Application.Services;

public class TokenService(IConfiguration configuration)
{
    public JwtToken UpdateRefreshToken(User user)
    {
        var accessToken = CreateAccessToken(user);
        var refreshToken = UpdateRefreshToken();
        return new JwtToken(accessToken, refreshToken);
    }
    
    public string CreateAccessToken(User user)
    {
        var token = user.CreateClaims().CreateJwtToken(configuration);
        return CompileJwtSecurityToken(token);
    }

    private string CompileJwtSecurityToken(JwtSecurityToken jwtSecurityToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(jwtSecurityToken);
    }

    private string UpdateRefreshToken()
    {
        return JwtBearerExtensions.GenerateRefreshToken();
    }

    public ClaimsPrincipal? GetPrincipalClaims(string accessToken) => 
        configuration.GetPrincipalFromExpiredToken(accessToken);
}