namespace ServiceCompany.Models
{
    public class IndexViewModel : BaseViewModel
    {
        public List<UserViewModel> Users { get; set; }

        internal List<ProjectViewModel> Projects { get; set; }

        internal List<TaskViewModel> AllTasks { get; set; }

        internal List<TaskViewModel> CompletedTasks { get; set; }

        internal List<TaskViewModel> WorkInProgressTasks { get; set; }
    }
}
