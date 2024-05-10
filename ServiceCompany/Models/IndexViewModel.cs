using ServiceCompany.ApiServices;

namespace ServiceCompany.Models
{
    public class IndexViewModel : BaseViewModel
    {
        public int Number {  get; set; }

        public string JustFact { get; set; }

        public string MathFact { get; set; }

        public WeatherViewModel WeatherViewModel { get; set; }

        public List<UserViewModel> Users { get; set; }

        internal List<ProjectViewModel> Projects { get; set; }

        internal List<TaskViewModel> AllTasks { get; set; }

        internal List<TaskViewModel> CompletedTasks { get; set; }

        internal List<TaskViewModel> WorkInProgressTasks { get; set; }
    }
}
