namespace ServiceCompany.DbStuff.Models
{
    public class UserTaskStatus : BaseModelAbstract
    {
        public string Status { get; set; }

        public virtual List<UserTask>? UserTasks { get; set; } = new List<UserTask>();

        public UserTaskStatus() : base() { }
    }
}
