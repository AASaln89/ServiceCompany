using ServiceCompany.DbStuff.Models;

namespace ManagementCompany.DbStuff.ExternalEntities
{
    public class Warning : BaseEntityAbstract
    {
        public virtual DocumentEntity Document { get; set; }

        public virtual List<WarningElementEntity> FailureElements { get; set; } = new List<WarningElementEntity>();
    }
}
