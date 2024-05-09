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
            var localLanguageFromCookie = context.Request.Cookies["languages"];

            if (localLanguageFromCookie is null)
            {
                string acceptLanguage = context.Request.Headers.AcceptLanguage;
                var localLanguage = acceptLanguage.Substring(0, 5);

                culture = new CultureInfo(localLanguage);
            }
            else
            {
                culture = new CultureInfo(localLanguageFromCookie);
            }

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            await _next.Invoke(context);
        }
    }
}
