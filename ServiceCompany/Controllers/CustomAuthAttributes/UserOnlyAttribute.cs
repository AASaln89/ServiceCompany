using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceCompany.Services;

namespace ServiceCompany.Controllers.CustomAuthAttributes
{
    public class AdminOnlyAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authService = context.HttpContext.RequestServices.GetService<AuthService>();
            if (!authService.IsAdmin())
            {
                context.Result = new ForbidResult(AuthController.AUTH_KEY);
            }
        }
    }
}
