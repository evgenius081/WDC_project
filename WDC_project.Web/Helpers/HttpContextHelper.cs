using System;
using System.Linq;
using System.Security.Claims;

namespace WDC_project.Web.Helpers
{
    public class HttpContextHelper
    {
        public static int GetIdByContextUser(ClaimsPrincipal userContext)
        {
            var nameIdentifierClaim = userContext.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier);
            if (nameIdentifierClaim != null)
            {
                if (int.TryParse(nameIdentifierClaim.Value, out int userId))
                {
                    return userId;
                }
            }
            throw new ArgumentException("No User id in HttpContext User");
        }

        public static string GetNameByContextUser(ClaimsPrincipal userContext)
        {
            var nameIdentifierClaim = userContext.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name);
            if (nameIdentifierClaim != null)
            {
                return nameIdentifierClaim.Value;
            }
            throw new ArgumentException("No User name in HttpContext User");
        }

        public static int GetAgeByContextUser(ClaimsPrincipal userContext)
        {
            var nameIdentifierClaim = userContext.Claims.FirstOrDefault(u => u.Type == "Age");
            if (nameIdentifierClaim != null)
            {
                if (int.TryParse(nameIdentifierClaim.Value, out int userAge))
                {
                    return userAge;
                }
            }
            throw new ArgumentException("No User age in HttpContext User");
        }
    }
}