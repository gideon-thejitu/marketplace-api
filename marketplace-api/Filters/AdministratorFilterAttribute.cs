using Microsoft.AspNetCore.Mvc.Filters;

namespace marketplace_api.Filters;

public class AdministratorFilter : IAuthorizationFilter, IAdministratorFilterAttribute
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context != null)
        {
            Console.WriteLine("Yeey");
        }
    }
}