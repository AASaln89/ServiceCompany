using ServiceCompany.DbStuff.Models;
using System.Xml.Linq;

namespace ManagementCompany.DbStuff.ExternalEntities
{
    public class ElementEntity : BaseEntityAbstract
    {
        public string? FamilyName { get; set; }

        public int? ElementId { get; set; }

        public string? ElementType { get; set; }

        public string? Building { get; set; }

        public string? Section { get; set; }

        public string? Floor { get; set; }

        public string? Description { get; set; }

        public string? ClassifierPosition { get; set; }

        public string? WBSPosition { get; set; }

        public string? SetPosition { get; set; }

        public string? NameInCatalog { get; set; }

        public string? IdInCatalog { get; set; }

        public string? Manufacture { get; set; }

        public string? Mark { get; set; }

        public string? Material { get; set; }

        public string? ConstructionMaterial { get; set; }

        public string? DesignOption { get; set; }

        public string? DesignOptionId { get; set; }

        public string? BaseLevel { get; set; }

        public string? SettingLevel { get; set; }

        public string? PhaseCreated { get; set; }

        public string? PhaseDemolished { get; set; }

        public string? WorkSet { get; set; }

        public double? Volume { get; set; }

        public double? Square { get; set; }

        public double? Length { get; set; }

        public double? Height { get; set; }

        public double? Width { get; set; }

        public double? Perimeter { get; set; }

        public double? Radius { get; set; }

        public double? Diameter { get; set; }

        public double? Angle { get; set; }

        public bool? OnNavisworksView { get; set; }

        public virtual DocumentEntity Document { get; set; }

        public virtual LevelEntity Level { get; set; }

        public virtual CategoryEntity Category { get; set; }

        public virtual ElementTypeEntity Type { get; set; }

        public virtual List<MaterialEntity> Materials { get; set; } = new List<MaterialEntity>();

        public virtual List<ParameterEntity> Parameters { get; set; } = new List<ParameterEntity>();

        public ElementEntity() : base() { }
    }
}
