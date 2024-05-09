namespace ServiceCompany.DbStuff.Models
{
    public class Company : BaseModelAbstract
    {
        public string ShortName { get; set; }

        public virtual CompanyProfile Profile { get; set; }

        public virtual List<Project>? Projects { get; set; } = new List<Project>();

        public virtual List<User>? Users { get; set; } = new List<User>();
    }
}
