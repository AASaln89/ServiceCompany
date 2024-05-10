using ServiceCompany.DbStuff.Models;
using ServiceCompany.DbStuff.Models.Enums;
using ServiceCompany.DbStuff.Repositories;
using ServiceCompany.Models;

namespace ServiceCompany.BusinessServices
{
    public class UserBusinessService
    {
        private UserRepository _userRepository;
        private UserProfileRepository _userProfileRepository;
        private CompanyRepository _companyRepository;
        private ProjectRepository _projectRepository;
        private MemberRoleRepository _memberPermissionRepository;

        public UserBusinessService(UserRepository userRepository,
            CompanyRepository companyRepository,
            ProjectRepository projectRepository,
            MemberRoleRepository memberPermissionRepository,
            UserProfileRepository userProfileRepository)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _projectRepository = projectRepository;
            _memberPermissionRepository = memberPermissionRepository;
            _userProfileRepository = userProfileRepository;
        }

        public List<UserViewModel> GetUsersAndManagers()
        {
            var dbUsers = _userProfileRepository.GetUsers();

            var dbManagers = _userProfileRepository.GetManagers();

            var dbAllUsers = dbUsers.Concat(dbManagers);

            var viewModels = dbAllUsers
                .Select(dbUser => new UserViewModel
                {
                    Id = dbUser.Id,
                    NickName = dbUser.NickName,
                    MemberRole = dbUser.User?.MemberRole?.Role,
                })
                .ToList();

            return viewModels;
        }

        public List<UserViewModel> GetUsers()
        {
            var dbUsers = _userProfileRepository.GetUsers();

            var viewModels = dbUsers
                .Select(dbUser => new UserViewModel
                {
                    Id = dbUser.Id,
                    NickName = dbUser.NickName,
                    MemberRole = dbUser.User?.MemberRole?.Role,
                })
                .ToList();

            return viewModels;
        }

        public List<UserViewModel> GetManagers()
        {
            var dbManagers = _userProfileRepository.GetManagers();

            var viewModels = dbManagers
                .Select(dbUser => new UserViewModel
                {
                    Id = dbUser.Id,
                    NickName = dbUser.NickName,
                    MemberRole = dbUser.User?.MemberRole?.Role,
                })
                .ToList();

            return viewModels;
        }

        public int AddExecutor(ExecutorViewModel executorViewModel/*,int companyId, int projectId, int permissionId*/)
        {
            //var project = _projectRepository.GetById(projectId);

            var executor = new User
            {
                Email = executorViewModel.Email,
                Password = executorViewModel.Password,
                PreferLocalLang = "en",
                ExpireDate = executorViewModel.ExpireDate,
            };
            var executorProfile = new UserProfile();
            executorProfile.User = executor;

            return _userRepository.Add(executor);
        }

        public void DeleteExecutor(int id)
        {
            _userRepository.Delete(id);
        }
        
    }
}
