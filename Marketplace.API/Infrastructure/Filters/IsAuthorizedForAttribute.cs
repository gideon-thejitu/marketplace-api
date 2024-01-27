using Marketplace.Infrastructure.Authorization;
using Marketplace.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Marketplace.Infrastructure.Filters;

public class IsAuthorizedForAttribute : ActionFilterAttribute
{
    private string Resource { get; }
    private ICollection<string> Actions { get; }
    
    public IsAuthorizedForAttribute(string resource, params string[] actions)
    {
        Resource = resource;
        Actions = actions;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cerbosHandler = context.HttpContext.RequestServices.GetRequiredService<ICerbosHandler>();
        var currentUserService = context.HttpContext.RequestServices.GetRequiredService<ICurrentUserService>();

        var userEmail = currentUserService.GetCurrentUserEmail();

        if (userEmail is null)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Result = new JsonResult(new
            {
                Message = "Not allowed!"
            });
            
            return;
        }

        var isAllowed = await cerbosHandler.Handle(userEmail, Resource, Actions.ToArray());

        if (isAllowed is false)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Result = new JsonResult(new
            {
                Message = "Not allowed!"
            });
            return;
        }
        
        context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
    }
}