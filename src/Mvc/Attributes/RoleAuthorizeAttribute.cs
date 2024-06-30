using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mvc.Attributes;

public class RoleAuthorizeAttribute(string role) : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userRole = context.HttpContext.Request.Cookies["Role"];
        if (userRole != role)
        {
            if (userRole == "admin")
            {
                context.Result = new RedirectToActionResult("Index", "Users", null);
            }
            else
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }
    }
}