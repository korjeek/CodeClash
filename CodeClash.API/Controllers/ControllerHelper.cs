using System.Security.Claims;
using CodeClash.Application.Extensions;

namespace CodeClash.API.Controllers;

public static class ControllerHelper
{
    public static Guid GetUserIdFromAuthorizedUserCookie(this HttpRequest request)
    {
        request.Cookies.TryGetValue("spooky-cookies", out var cookie);
        return new Guid(cookie!.GetClaimsFromToken().First(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}