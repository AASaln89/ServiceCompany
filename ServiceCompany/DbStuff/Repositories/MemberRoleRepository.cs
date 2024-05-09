using ServiceCompany.DbStuff.Models;
using ServiceCompany.Models;

namespace ServiceCompany.DbStuff.Repositories
{
    public class MemberRoleRepository : BaseRepository<MemberRole>
    {
        public MemberRoleRepository(ServiceCompanyDbContext context) : base(context) { }

        public void UpdatePermission(RoleViewModel viewModel, int id)
        {
            var permission = _context.MemberRoles.Single(x => x.Id == id);

            permission.Role = viewModel.Permission;

            _context.SaveChanges();
        }
    }
}
