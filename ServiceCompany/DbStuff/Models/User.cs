using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceCompany.DbStuff.Models
{
    public class User : BaseModelAbstract
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string PreferLocalLang { get; set; }

        public DateTime? ExpireDate { get; set; }

        public virtual Company? Company { get; set; }

        public virtual User? Author { get; set; }

        public virtual UserProfile? Profile { get; set; }

        public virtual List<Project>? Projects { get; set; }

        public virtual List<Project>? CreatedProjects { get; set; }

        public virtual List<Company>? CreatedCompanies { get; set; }

        public virtual MemberRole? MemberRole { get; set; }

        public virtual List<UserTask>? CreatedTasks { get; set; }

        public virtual List<UserTask>? ExecutedTasks { get; set; }

        public virtual List<Article>? Articles { get; set; }

        public virtual List<Alert>? SeenAlerts { get; set; }

        public virtual List<Alert>? CreatedAlerts { get; set; }

        public User() : base() { }
    }
}
