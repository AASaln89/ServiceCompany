using ServiceCompany.DbStuff.Models;

namespace ManagementCompany.DbStuff.ExternalEntities
{
    public class WarningElementEntity : BaseEntityAbstract
    {
        public int? ElementId { get; set; }

        public virtual Warning Warning { get; set; }

        public WarningElementEntity() : base() { }
    }
}
