using ServiceCompany.DbStuff.Models;

namespace ManagementCompany.DbStuff.ExternalEntities
{
    public class ElementTypeEntity : BaseEntityAbstract
    {
        public int? ElementId { get; set; }

        public string ElementType { get; set; }

        public virtual DocumentEntity Document { get; set; }

        public virtual CategoryEntity Category { get; set; }

        public virtual List<GridEntity> Grids { get; set; } = new List<GridEntity>();

        public virtual List<LevelEntity> Levels { get; set; } = new List<LevelEntity>();

        public virtual List<ElementEntity> Elements { get; set; } = new List<ElementEntity>();
    }
}
