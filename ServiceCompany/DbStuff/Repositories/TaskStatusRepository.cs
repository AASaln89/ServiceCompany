using ServiceCompany.DbStuff.Models;
using ServiceCompany.Models;

namespace ServiceCompany.DbStuff.Repositories
{
    public class TaskStatusRepository : BaseRepository<UserTaskStatus>
    {
        public TaskStatusRepository(ServiceCompanyDbContext context) : base(context) { }

        //public void UpdateStatus(StatusViewModel viewModel, int id)
        //{
        //    var status = _context.TaskStatuses.Single(x => x.Id == id);

        //    status.Status = viewModel.Status;

        //    _context.SaveChanges();
        //}
    }
}
