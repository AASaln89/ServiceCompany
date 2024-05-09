using ServiceCompany.DbStuff.Models;

namespace ManagementCompany.DbStuff.ExternalEntities
{
    public class MaterialEntity : BaseEntityAbstract
    {
        public int MaterialId { get; set; }

        public double? Volume { get; set; }

        public double? Square { get; set; }

        public virtual DocumentEntity Document { get; set; }

        public virtual List<ElementEntity> Elements { get; set; } = new List<ElementEntity>();

        public MaterialEntity() : base() { }
    }
}
