using ServiceCompany.DbStuff.Models;

namespace ManagementCompany.DbStuff.ExternalEntities
{
    public class RevitCategoryEntity : BaseEntityAbstract
    {
        public int CategoryId { get; set; }

        public string CategoryType { get; set; }

        public RevitCategoryEntity() : base() { }
    }
}
