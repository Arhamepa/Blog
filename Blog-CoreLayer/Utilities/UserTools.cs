using System.Security.Claims;

namespace Blog_CoreLayer.Utilities;

public static class UserTools
{
    public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal == null)
        {
            throw new ArgumentNullException(nameof(claimsPrincipal));
        }

        return Convert.ToInt32(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}