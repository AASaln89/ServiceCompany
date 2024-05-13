using ManagementCompany.DbStuff.ExternalEntities;

namespace ServiceCompany.DbStuff.Models
{
    public class Project : BaseModelAbstract
    {
        public string? ShortName { get; set; }

        public string? Address { get; set; }

        public virtual Company? Company { get; set; }

        public virtual User? Author { get; set; }

        public virtual List<DocumentEntity> Documents { get; set; } = new List<DocumentEntity>();

        public virtual List<User>? Users { get; set; } = new List<User>();

        public Project() : base() { }
    }
}
