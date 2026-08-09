using System.Security.Claims;

namespace PersonalDigitalVault_WebApplication.Helpers
{
    public static class UserHelper
    {
        public static int GetUserId(ClaimsPrincipal user)
        {
            var value = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(value!);
        }

        public static string GetRole(ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Role) ?? "User";
        }
    }
}