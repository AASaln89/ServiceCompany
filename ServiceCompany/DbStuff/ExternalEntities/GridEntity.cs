using ServiceCompany.DbStuff.Models;

namespace ManagementCompany.DbStuff.ExternalEntities
{
    public class GridEntity : BaseEntityAbstract
    {
        public int GridId { get; set; }

        public double LocationX { get; set; }

        public double LocationY { get; set; }

        public double LocationZ { get; set; }

        public virtual DocumentEntity Document { get; set; }

        public virtual ElementTypeEntity ElementType { get; set; }

        public virtual CategoryEntity Category { get; set; }

        public virtual List<ElementEntity> Elements { get; set; } = new List<ElementEntity>();
    }
}
