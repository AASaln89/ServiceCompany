using ServiceCompany.DbStuff.Models;
using ServiceCompany.DbStuff.Models.Enums;
using ServiceCompany.DbStuff.Repositories;

namespace ServiceCompany.Services
{
    public class AuthService
    {
        private UserRepository _userRepository;
        private IHttpContextAccessor _httpContextAccessor;
        public const string LOCAL_LANG_TYPE = "localLanguage";
        public const string ROLE = "roleName";
        public const string ROLE_ID = "roleId";
        public const string NAME = "name";
        public const string EMAIL = "email";
        public const string ID = "id";

        public AuthService(IHttpContextAccessor httpContextAccessor,
            UserRepository userRepository)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;// HttpContext == null
            _userRepository = userRepository;
        }

        public User? GetCurrentUser()
        {
            var id = GetCurrentUserId();

            if (id == null)
            {
                return null;
            }

            return _userRepository.GetById(id.Value);
        }

        public int? GetCurrentUserId()
        {
            // HttpContext != null
            var idStr = _httpContextAccessor?.HttpContext?.User.Claims?.FirstOrDefault(x => x.Type == ID)?.Value;

            if (idStr == null)
            {
                return null;
            }
            var id = int.Parse(idStr);

            return id;
        }

        public string GetCurrentPermissionName()
        {
            return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ROLE)?.Value ?? "Гость";
        }

        public string GetCurrentUserNickName()
        {
            return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == NAME)?.Value ?? "Гость";
        }

        public bool IsSuperAdmin()
        {
            return GetCurrentPermissionName() == MemberRoleEnum.SuperAdmin.ToString();
        }

        public bool IsAdmin()
        {
            if (GetCurrentPermissionName() == MemberRoleEnum.Admin.ToString() || GetCurrentPermissionName() == MemberRoleEnum.SuperAdmin.ToString())
            {

                return true;
            }
            else 
            {
                return false;
            }
        }

        public bool IsExecutor()
        {
            return GetCurrentPermissionName() == MemberRoleEnum.Executor.ToString();
        }

        public bool IsManager()
        {
            return GetCurrentPermissionName() == MemberRoleEnum.Manager.ToString();
        }

        public bool IsUser()
        {
            return GetCurrentPermissionName() == MemberRoleEnum.User.ToString();
        }

        public bool IsAuthenticated()
        {
            return GetCurrentUserId() != null;
        }

        public User GetCurrentMcUser()
        {
            var idStr = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ID).Value;
            var id = int.Parse(idStr);
            return _userRepository.GetById(id);
        }

        public string GetCurrentUserLocalLanguage()
        {
            return _httpContextAccessor.HttpContext.User
                .Claims.FirstOrDefault(x => x.Type == LOCAL_LANG_TYPE)
                ?.Value ?? "en";
        }

        public bool IsAuthorized()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }
    }
}
