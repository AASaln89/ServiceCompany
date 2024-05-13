using ServiceCompany.DbStuff.Models;
using ServiceCompany.DbStuff.Models.Enums;
using ServiceCompany.DbStuff.Repositories;

namespace ServiceCompany.DbStuff
{
    public static class SeedExtension
    {
        public static void Seed(WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                SeedRole(serviceScope.ServiceProvider);
                SeedUser(serviceScope.ServiceProvider);
                SeedCompany(serviceScope.ServiceProvider);
                SeedProject(serviceScope.ServiceProvider);
            }
        }

        private static void SeedRole(IServiceProvider di)
        {
            var roleRepository = di.GetService<MemberRoleRepository>();

            if (roleRepository.Any() == false)
            {
                foreach (string memberRole in Enum.GetNames(typeof(MemberRoleEnum)))
                {
                    var role = new MemberRole()
                    {
                        Role = memberRole,
                    };

                    roleRepository.Add(role);
                }
            }
        }

        private static void SeedUser(IServiceProvider di)
        {
            var roleRepository = di.GetService<MemberRoleRepository>();
            var userRepository = di.GetService<UserRepository>();
            var userProfileRepository = di.GetService<UserProfileRepository>();

            if (userRepository.Any() == false)
            {
                var superAdmin = new User
                {
                    Name = MemberRoleEnum.SuperAdmin.ToString(),
                    Email = "SuperAdmin",
                    Password = "SuperAdmin",
                    PreferLocalLang = "en",
                    MemberRole = roleRepository?.GetById((int)MemberRoleEnum.SuperAdmin),
                };
                var superAdminProfile = new UserProfile()
                {
                    NickName = MemberRoleEnum.SuperAdmin.ToString(),
                    FirstName = "Super",
                    LastName = "Admin",
                    User = superAdmin,
                };
                userProfileRepository.Add(superAdminProfile);

                var admin = new User
                {
                    Name = MemberRoleEnum.Admin.ToString(),
                    Email = "Admin",
                    Password = "Admin",
                    PreferLocalLang = "en",
                    MemberRole = roleRepository?.GetById((int)MemberRoleEnum.Admin)
                };
                var adminProfile = new UserProfile
                {
                    NickName = MemberRoleEnum.Admin.ToString(),
                    FirstName = "Admin",
                    LastName = "Admin",
                    User = admin,
                };
                userProfileRepository.Add(adminProfile);

                var manager = new User
                {
                    Name = MemberRoleEnum.Manager.ToString(),
                    Email = "Manager",
                    Password = "Manager",
                    PreferLocalLang = "en",
                    MemberRole = roleRepository?.GetById((int)MemberRoleEnum.Manager)
                };
                var managerProfile = new UserProfile
                {
                    NickName = MemberRoleEnum.Manager.ToString(),
                    FirstName = "Manager",
                    LastName = "Manager",
                    User = manager,
                };
                userProfileRepository.Add(managerProfile);

                var user = new User
                {
                    Name = MemberRoleEnum.User.ToString(),
                    Email = "User",
                    Password = "User",
                    PreferLocalLang = "en",
                    MemberRole = roleRepository?.GetById((int)MemberRoleEnum.User)
                };
                var userProfile = new UserProfile
                {
                    NickName = MemberRoleEnum.User.ToString(),
                    FirstName = "User",
                    LastName = "User",
                    User = user,
                };
                userProfileRepository.Add(userProfile);
            }
        }

        private static void SeedCompany(IServiceProvider di)
        {
            var userRepository = di.GetService<UserRepository>();
            var companyRepository = di.GetService<CompanyRepository>();

            if (companyRepository.Any() == false)
            {
                var testCompanyProfile = new CompanyProfile()
                {
                    Email = "TestCompany@email.com",
                    Address = "TestCompanyAddress",
                    PhoneNumber = "TestCompanyPhoneNumber",
                };
                var testCompany = new Company
                {
                    Name = "TestCompany",
                    ShortName = "TestC1",
                    Profile = testCompanyProfile,
                    Author = userRepository.GetSuperAdmin()
                };
                companyRepository.Add(testCompany);
            }
        }

        private static void SeedProject(IServiceProvider di)
        {
            var userRepository = di.GetService<UserRepository>();
            var companyRepository = di.GetService<CompanyRepository>();
            var projectRepository = di.GetService<ProjectRepository>();

            if (projectRepository.Any() == false)
            {
                var testProject = new Project
                {
                    Name = "TestProject",
                    ShortName = "TestP1",
                    Address = "TestProjectAddress",
                    Company = companyRepository.GetTestCompany(),
                    Author = userRepository.GetSuperAdmin()
                };
                projectRepository.Add(testProject);
            }
        }
    }
}
