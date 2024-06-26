using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;

namespace Mvc.Attributes;

public class AuthenticateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var endpoint = context.HttpContext.GetEndpoint();
        if (endpoint != null)
        {
            var allowAnonymous = endpoint.Metadata.GetMetadata<IAllowAnonymous>();
            if (allowAnonymous != null)
            {
                return; // Skip authentication check if [AllowAnonymous] is present
            }
        }

        var hasAccessToken = context.HttpContext.Request.Cookies.ContainsKey("AccessToken");
        if (!hasAccessToken)
        {
            context.Result = new RedirectToActionResult("Login", "Auth", null);
        }
    }
}