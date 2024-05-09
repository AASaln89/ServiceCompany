using ServiceCompany.DbStuff.Models;
using ServiceCompany.DbStuff.Models.Enums;
using ServiceCompany.DbStuff.Repositories;

namespace ServiceCompany.DbStuff
{
    public static class SeedExtention
    {
        public static void Seed(WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                SeedRole(serviceScope.ServiceProvider);
                SeedUser(serviceScope.ServiceProvider);
            }
        }

        private static void SeedRole(IServiceProvider di)
        {
            var permissionRepository = di.GetService<MemberRoleRepository>();

            if (permissionRepository.Any() == false)
            {
                foreach (string memberPermission in Enum.GetNames(typeof(MemberRoleEnum)))
                {
                    var permission = new MemberRole();
                    permission.Role = memberPermission.Replace('Z', ' ');

                    permissionRepository.Add(permission);
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
                    MemberRole = roleRepository?.GetById((int)MemberRoleEnum.SuperAdmin),
                };
                var superAdminProfile = new UserProfile()
                {
                    NickName = MemberRoleEnum.SuperAdmin.ToString(),
                };
                superAdminProfile.User = superAdmin;
                userProfileRepository.Add(superAdminProfile);

                var admin = new User
                {
                    Name = MemberRoleEnum.Admin.ToString(),
                    Email = "Admin",
                    Password = "Admin",
                    MemberRole = roleRepository?.GetById((int)MemberRoleEnum.Admin)
                };
                var adminProfile = new UserProfile();
                adminProfile.User = admin;
                userProfileRepository.Add(adminProfile);

                var manager = new User
                {
                    Name = MemberRoleEnum.Manager.ToString(),
                    Email = "Manager",
                    Password = "Manager",
                    MemberRole = roleRepository?.GetById((int)MemberRoleEnum.Manager)
                };
                var managerProfile = new UserProfile();
                managerProfile.User = manager;
                userProfileRepository.Add(managerProfile);

                var user = new User
                {
                    Name = MemberRoleEnum.User.ToString(),
                    Email = "User",
                    Password = "User",
                    MemberRole = roleRepository?.GetById((int)MemberRoleEnum.User)
                };
                var userProfile = new UserProfile();
                userProfile.User = user;
                userProfileRepository.Add(userProfile);
            }
        }
    }
}
