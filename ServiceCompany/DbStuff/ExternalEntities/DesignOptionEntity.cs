using ServiceCompany.DbStuff.Models;

namespace ManagementCompany.DbStuff.ExternalEntities
{
    public class DesignOptionEntity : BaseEntityAbstract
    {
        public int DesignOptionId { get; set; }

        public bool IsPrimary { get; set; }

        public virtual DocumentEntity Document { get; set; }

        public virtual CategoryEntity Category { get; set; }
    }
}

