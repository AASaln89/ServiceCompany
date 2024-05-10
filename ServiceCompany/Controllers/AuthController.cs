using ServiceCompany.DbStuff.Models.Enums;
using ServiceCompany.DbStuff.Repositories;
using ServiceCompany.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ServiceCompany.Services;
using Microsoft.AspNetCore.Authorization;
using ServiceCompany.DbStuff.Models;

namespace ServiceCompany.Controllers
{
    public class AuthController : Controller
    {
        private UserRepository _userRepository;
        private AuthService _authService;
        private MemberRoleRepository _memberRoleRepository;

        public const string AUTH_KEY = "McCompany";

        public AuthController(UserRepository mcUserRepository, 
            AuthService authService, 
            MemberRoleRepository memberRoleRepository)
        {
            _userRepository = mcUserRepository;
            _authService = authService;
            _memberRoleRepository = memberRoleRepository;
        }

        public IActionResult Registration()
        {
            var model = new RegistrationViewModel();
            CheckUser(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel registrationViewModel)
        {
            CheckUser(registrationViewModel);

            if (!ModelState.IsValid)
            {
                return View(registrationViewModel);
            }

            var user = new User
            {
                Email = registrationViewModel.Email,
                Password = registrationViewModel.Password,
                PreferLocalLang = "en",
                MemberRole = _memberRoleRepository.GetById((int)MemberRoleEnum.User),
            };
            var userProfile = new UserProfile
            {
                NickName = registrationViewModel.NickName,
            };
            user.Profile = userProfile;

            _userRepository.Add(user);

            return RedirectToAction("Login");
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
                return RedirectToAction("LogError", "Auth");
            }

            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("permissionId", user.MemberRole?.Id.ToString() ?? ""),
                new Claim("permissionName", user.MemberRole?.Role ?? ""),
                new Claim(AuthService.LOCAL_LANG_TYPE, user.PreferLocalLang),
                new Claim("Name", user.Name ?? ""),
                new Claim("email", user.Email ?? ""),
            };

            var identity = new ClaimsIdentity(claims, AUTH_KEY);
            var principal = new ClaimsPrincipal(identity);
            HttpContext
                .SignInAsync(AUTH_KEY, principal)
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

        [HttpPost]
        public IActionResult ReLogin(string email, string password)
        {
            var user = _userRepository.GetLogUser(email, password);
            if (user == null)
            {
                return RedirectToAction("LogError", "ServiceCompany");
            }

            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("permissionId", user.MemberRole?.Id.ToString() ?? ""),
                new Claim("permissionName", user.MemberRole?.Role ?? ""),
                new Claim(AuthService.LOCAL_LANG_TYPE, user.PreferLocalLang),
                new Claim("Name", user.Name ?? ""),
                new Claim("email", user.Email ?? ""),
            };

            var identity = new ClaimsIdentity(claims, AUTH_KEY);
            var principal = new ClaimsPrincipal(identity);
            HttpContext
                .SignInAsync(AUTH_KEY, principal)
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

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Index", "ServiceCompany");
        }

        [Authorize]
        public IActionResult SwitchLocalLanguage(string localLang)
        {
            var user = _authService.GetCurrentUser();
            var userId = _authService.GetCurrentUserId().Value;
            _userRepository.SwitchLocalLanguage(userId, localLang);

            Logout();
            ReLogin(user.Email, user.Password);

            return RedirectToAction("Profile", "ServiceCompany");
        }

        public IActionResult LogError()
        {
            var model = new BaseViewModel();
            CheckUser(model);
            return View(model);
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
