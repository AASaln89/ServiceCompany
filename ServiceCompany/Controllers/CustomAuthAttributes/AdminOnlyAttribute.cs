using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceCompany.Services;

namespace ServiceCompany.Controllers.CustomAuthAttributes
{
    public class UserOnlyAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authService = context.HttpContext.RequestServices.GetService<AuthService>();
            if (!authService.IsUser())
            {
                context.Result = new ForbidResult(AuthController.AUTH_KEY_MC);
            }
        }
    }
}
