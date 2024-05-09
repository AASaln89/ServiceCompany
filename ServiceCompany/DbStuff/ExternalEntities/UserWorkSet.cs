using ServiceCompany.DbStuff.Models;

namespace ManagementCompany.DbStuff.ExternalEntities
{
    public class UserWorkSet : BaseEntityAbstract
    {
        public int WorkSetId { get; set; }

        public virtual DocumentEntity Document { get; set; }
    }
}
