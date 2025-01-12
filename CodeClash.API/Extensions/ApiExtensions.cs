using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CodeClash.API.Extensions;

public static class ApiExtensions
{
    public static Guid GetUserIdFromAccessToken(this ClaimsPrincipal claims) =>
        new Guid(claims.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
    
    public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    LifetimeValidator = CustomLifetimeValidator,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["spooky-cookies"];
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization();
    }
    
    private static bool CustomLifetimeValidator(
        DateTime? notBefore,
        DateTime? expires,
        SecurityToken securityToken,
        TokenValidationParameters validationParameters)
    {
        if (expires != null && DateTime.UtcNow > expires)
        {
            Console.WriteLine("Token has expired.");
            return false;
        }

        if (notBefore != null && DateTime.UtcNow < notBefore)
        {
            Console.WriteLine("Token is not yet valid.");
            return false;
        }

        // Дополнительная кастомная логика
        Console.WriteLine("Token lifetime is valid.");
        return true;
    }
}