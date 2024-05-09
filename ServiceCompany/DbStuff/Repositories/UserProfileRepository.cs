using Microsoft.EntityFrameworkCore;
using ServiceCompany.Models;
using ServiceCompany.DbStuff.Models;
using ServiceCompany.DbStuff.Models.Enums;

namespace ServiceCompany.DbStuff.Repositories
{
    public class UserProfileRepository : BaseRepository<UserProfile>
    {
        public UserProfileRepository(ServiceCompanyDbContext context) : base(context) { }

        public override UserProfile GetById(int id)
        {
            return _entities.Include(x => x.User).SingleOrDefault(ent => ent.Id == id);
        }

        public override IEnumerable<UserProfile> GetAll()
        {
            return _entities
                .Include(x => x.User)
                .ThenInclude(x=>x.MemberRole)
                .ToList();
        }

        public IEnumerable<UserProfile> GetManagers()
        {
            return _entities
                .Include(x => x.User)
                .ThenInclude(x => x.MemberRole)
                .Where(x => x.User.MemberRole.Id == (int)MemberRoleEnum.Manager)
                .ToList();
        }

        public IEnumerable<UserProfile> GetUsers()
        {
            return _entities
                .Include(x => x.User)
                .ThenInclude(x => x.MemberRole)
                .Where(x => x.User.MemberRole.Id == (int)MemberRoleEnum.User)
                .ToList();
        }

        public void UpdateProfileUser(ProfileViewModel model, int id)
        {
            var user = _context.UserProfiles.SingleOrDefault(x => x.Id == id);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.NickName = model.NickName;
            user.User.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.User.Password = model.Password;

            _context.SaveChanges();
        }
    }
}
