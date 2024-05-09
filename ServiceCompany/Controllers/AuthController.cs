using ServiceCompany.DbStuff.Models.Enums;
using ServiceCompany.DbStuff.Repositories;
using ServiceCompany.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ServiceCompany.Controllers
{
    public class AuthController : Controller
    {
        private UserRepository _userRepository;

        public const string AUTH_KEY_MC = "McCompany";

        public AuthController(UserRepository mcUserRepository)
        {
            _userRepository = mcUserRepository;
        }

        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var user = _userRepository.GetLogUser(loginViewModel.Email, loginViewModel.Password);
            if (user == null)
            {
                //ModelState.AddModelError(nameof(LoginViewModel.Email), "Wrong name or password");
                //return View(loginViewModel);
                return RedirectToAction("LogError", "ServiceCompany");
            }

            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("permissionId", user.MemberRole?.Id.ToString() ?? ""),
                new Claim("permissionName", user.MemberRole?.Role ?? ""),
                new Claim("Name", user.Name ?? ""),
                new Claim("email", user.Email ?? ""),
            };

            var identity = new ClaimsIdentity(claims, AUTH_KEY_MC);
            var principal = new ClaimsPrincipal(identity);
            HttpContext
                .SignInAsync(AUTH_KEY_MC, principal)
                .Wait();

            if (user.MemberRole?.Id == (int)MemberRoleEnum.User)
            {
                return RedirectToAction("Profile", "ServiceCompany");
            }
            else
            {
                return RedirectToAction("AdminPanel", "ServiceCompany");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Index", "ServiceCompany");
        }
    }
}
