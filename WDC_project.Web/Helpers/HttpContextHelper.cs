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
    }
}