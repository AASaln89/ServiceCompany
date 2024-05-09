using Microsoft.EntityFrameworkCore;
using ServiceCompany.Models;
using ServiceCompany.DbStuff.Models;
using ServiceCompany.DbStuff.Models.Enums;

namespace ServiceCompany.DbStuff.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(ServiceCompanyDbContext context) : base(context) { }

        public override User GetById(int id)
        {
            return _entities.Include(x => x.MemberRole).SingleOrDefault(ent => ent.Id == id);
        }

        public override IEnumerable<User> GetAll()
        {
            return _entities
                .Include(x => x.MemberRole)
                .OrderBy(x => x.MemberRole)
                .ToList();
        }

        public void UpdateUser(ProfileViewModel model, int id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);

            user.Email = model.Email;
            user.Password = model.Password;
            user.Password = model.Password;

            _context.SaveChanges();
        }

        public IEnumerable<User> GetUsers()
        {
            return _entities
                .Include(x => x.MemberRole)
                .Include(x=>x.Profile)
                .Include(x => x.Company)
                .Where(x => x.MemberRole.Id == (int)MemberRoleEnum.User)
                .ToList();
        }

        public User GetExecutor(int id)
        {
            return _entities
                .Include(x => x.MemberRole)
                .Include(x => x.Company)
                .Where(x => x.MemberRole.Id != (int)MemberRoleEnum.User)
                .Where(x => x.MemberRole.Id != (int)MemberRoleEnum.SuperAdmin)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<User> GetExecutors()
        {
            return _entities
                .Include(x => x.MemberRole)
                .Include(x => x.Profile)
                .Include(x => x.Company)
                .Where(x => x.MemberRole.Id != (int)MemberRoleEnum.User)
                .Where(x => x.MemberRole.Id != (int)MemberRoleEnum.SuperAdmin)
                .ToList();
        }

        public IEnumerable<User> GetManagers()
        {
            return _entities
                .Include(x => x.MemberRole)
                .Include(x => x.Company)
                .Where(x => x.MemberRole.Id == (int)MemberRoleEnum.Manager)
                .ToList();
        }

        public User GetLogUser(string email, string password)
        {
            if (email is not null & password is not null)
            {
                var user = _context.Users.Include(x => x.MemberRole)
                .SingleOrDefault(x => x.Email.ToLower() == email.ToLower() && x.Password == password);

                return user;
            }
            else
            {
                return null;
            }
        }

        public void UpdateExecutor(ExecutorViewModel viewModel, int id, int statusId, int companyId, int projectId, int permissionId)
        {
            var executor = _context.Users.Include(x => x.Company).First(x => x.Id == id);

            executor.Id = id;
            executor.Email = viewModel.Email;
            executor.Password = viewModel.Password;
            executor.ExpireDate = viewModel.ExpireDate;
            executor.MemberRole = _context.MemberRoles.Single(x => x.Id == permissionId);
            executor.Company = _context.Companies.Single(x => x.Id == companyId);

            _context.SaveChanges();
        }
    }
}
