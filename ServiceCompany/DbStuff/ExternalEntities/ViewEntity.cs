using ServiceCompany.DbStuff.Models;

namespace ManagementCompany.DbStuff.ExternalEntities
{
    public class ViewEntity : BaseEntityAbstract
    {
        public int ViewId { get; set; }

        public string ViewType { get; set; }

        public bool OnSheet { get; set; }

        public virtual DocumentEntity Document { get; set; }

        public virtual CategoryEntity Category { get; set; }
    }
}
