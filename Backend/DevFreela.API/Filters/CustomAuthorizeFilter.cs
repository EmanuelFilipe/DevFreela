using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

public class CustomAuthorizeFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

        // Admin always has access
        if (roles.Contains("admin")) return;

        var endpoint = context.ActionDescriptor as ControllerActionDescriptor;
        var requiredRoles = endpoint.MethodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), false)
                                               .Cast<AuthorizeAttribute>()
                                               .Select(a => a.Roles)
                                               .FirstOrDefault()?
                                               .Split(',').Select(r => r.Trim()).ToList();

        if (requiredRoles != null && !requiredRoles.Any(role => roles.Contains(role)))
            context.Result = new ForbidResult();
    }
}