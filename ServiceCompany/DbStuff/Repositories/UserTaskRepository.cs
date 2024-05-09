using ServiceCompany.DbStuff.Models;
using ServiceCompany.DbStuff.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace ServiceCompany.DbStuff.Repositories
{
    public class UserTaskRepository : BaseRepository<UserTask>
    {
        public UserTaskRepository(ServiceCompanyDbContext context) : base(context) { }

        public IEnumerable<UserTask> GetInProgressTasks()
        {
            return _entities
                .Include(x => x.Status)
                .Where(x => x.Status.Id != (int)UserTaskStatusEnum.Complete);
        }

        public IEnumerable<UserTask> GetCompletedTasks()
        {
            return _entities
                .Include(x => x.Status)
                .Where(x => x.Status.Id == (int)UserTaskStatusEnum.Complete);
        }

        public IEnumerable<UserTask> GetCurrentUserTasks(User user)
        {
            return _entities
                .Include(t => t.Author)
                .Include(t => t.Status)
                .Where(t => t.Author.Id == user.Id)
                .ToList();
        }
    }
}
