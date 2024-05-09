using ServiceCompany.DbStuff.Models;

namespace ManagementCompany.DbStuff.ExternalEntities
{
    public class LevelEntity : BaseEntityAbstract
    {
        public int LevelId { get; set; }

        public double Elevation { get; set; }

        public virtual DocumentEntity Document { get; set; }

        public virtual CategoryEntity Category { get; set; }

        public virtual ElementTypeEntity ElementType { get; set; }

        public virtual HashSet<ElementEntity> Elements { get; set; } = new HashSet<ElementEntity>();
    }
}
