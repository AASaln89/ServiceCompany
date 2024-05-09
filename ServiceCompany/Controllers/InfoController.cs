using ServiceCompany.Models;
using ServiceCompany.Services;
using Microsoft.AspNetCore.Mvc;

namespace ServiceCompany.Controllers
{
    public class InfoController : Controller
    {
        private ReflectionService _reflectionService;
        private AuthService _authService;

        public InfoController(ReflectionService reflectionService, AuthService authService)
        {
            _reflectionService = reflectionService;
            _authService = authService;
        }

        public IActionResult Index()
        {
            var mcController = typeof(ServiceCompanyController);

            var apiHelperViewModel = _reflectionService
                .BuildApiHelperViewModel(mcController);

            CheckUser(apiHelperViewModel);

            return View(apiHelperViewModel);
        }

        private void CheckUser(BaseViewModel model)
        {
            var user = _authService.GetCurrentUser();
            if (user is not null)
            {
                if (user?.MemberRole?.Id == 5)
                {
                    model.IsUser = true;
                }
                else if (user?.MemberRole?.Id == 1)
                {
                    model.IsSuperAdmin = true;
                }
                else
                {
                    model.IsAdmin = true;
                }
            }
            else
            {
                model.IsGuest = true;
            }
        }
    }
}
