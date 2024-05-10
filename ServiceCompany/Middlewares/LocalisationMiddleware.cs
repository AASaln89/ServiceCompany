using ServiceCompany.Services;
using System.Globalization;

namespace ServiceCompany.Middlewares
{
    public class LocalisationMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalisationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            CultureInfo culture;

            var authService = context.RequestServices.GetService<AuthService>();
            if (authService.IsAuthorized())
            {
                culture = new CultureInfo(authService.GetCurrentUserLocalLanguage());
            }
            else if (context.Request.Cookies["languages"] is not null)
            {
                var localLanguageFromCookie = context.Request.Cookies["languages"];
                culture = new CultureInfo(localLanguageFromCookie);
            }
            else
            {
                string acceptLanguage = context.Request.Headers.AcceptLanguage;
                var localLanguage = acceptLanguage.Substring(0, 2);

                culture = new CultureInfo(localLanguage);
            }

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            await _next.Invoke(context);
        }
    }
}
