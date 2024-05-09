using ServiceCompany.DbStuff.Models;
using System.Security.Principal;

namespace ManagementCompany.DbStuff.ExternalEntities
{
    public class DocumentEntity : BaseEntityAbstract
    {
        public string Section { get; set; }

        public virtual Project Project { get; set; }

        public virtual List<ElementTypeEntity> ElementTypes { get; set; } = new List<ElementTypeEntity>();

        public virtual List<ElementEntity> Elements { get; set; } = new List<ElementEntity>();

        public virtual List<Warning> Warnings { get; set; } = new List<Warning>();

        public virtual List<ViewEntity> Views { get; set; } = new List<ViewEntity>();

        public virtual List<UserWorkSet> WorkSets { get; set; } = new List<UserWorkSet>();

        public virtual List<LevelEntity> Levels { get; set; } = new List<LevelEntity>();

        public virtual List<GridEntity> Grids { get; set; } = new List<GridEntity>();

        public virtual List<DesignOptionEntity> DesignOptions { get; set; } = new List<DesignOptionEntity>();

        public virtual List<LocationEntity> Locations { get; set; } = new List<LocationEntity>();

        public virtual List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();

        public virtual List<MaterialEntity> Materials { get; set; } = new List<MaterialEntity>();

        public virtual List<ParameterEntity> Parameters { get; set; } = new List<ParameterEntity>();

        public DocumentEntity() : base()
        {
        }

        public DocumentEntity(string documentSection) : base()
        {
            Section = documentSection;
        }
    }
}
