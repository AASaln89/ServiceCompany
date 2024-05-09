namespace ServiceCompany.DbStuff.Models
{
    public class UserTask : BaseModelAbstract
    {
        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public virtual UserTaskStatus? Status { get; set; }

        public virtual User? Author { get; set; }

        public virtual User? Executor { get; set; }

        public UserTask() : base() { }
    }
}
