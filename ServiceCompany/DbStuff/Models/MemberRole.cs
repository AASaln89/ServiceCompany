namespace ServiceCompany.DbStuff.Models
{
    public class MemberRole : BaseModelAbstract
    {
        public string? Role { get; set; }

        public virtual List<User>? Users { get; set; } = new List<User>();
    }
}
