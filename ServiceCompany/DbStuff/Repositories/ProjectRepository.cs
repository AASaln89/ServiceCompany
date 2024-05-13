using ServiceCompany.DbStuff.Models;
using Microsoft.EntityFrameworkCore;

namespace ServiceCompany.DbStuff.Repositories
{
    public class ProjectRepository : BaseRepository<Project>
    {
        public ProjectRepository(ServiceCompanyDbContext context) : base(context) { }

        public IEnumerable<Project> GetProjectsWithStatus()
        {
            return _entities
                .Include(x => x.Company)
                .Include(x=>x.Author)
                .ThenInclude(x=>x.Profile)
                .ToList();
        }
    }
}
