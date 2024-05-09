namespace ServiceCompany.Models.MongoViewModels
{
    public class IndexMongoViewModel : BaseViewModel
    {
        public List<FileJpgViewModel> FilesJpg { get; set; }

        public List<FileViewModel> Files { get; set; }

        internal List<ProjectViewModel> Projects { get; set; }

        internal List<TaskViewModel> AllTasks { get; set; }

        internal List<TaskViewModel> CompletedTasks { get; set; }

        internal List<TaskViewModel> WorkInProgressTasks { get; set; }
    }
}
