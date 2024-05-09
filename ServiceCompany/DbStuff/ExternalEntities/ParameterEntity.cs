using ServiceCompany.DbStuff.Models;

namespace ManagementCompany.DbStuff.ExternalEntities
{
    public class ParameterEntity : BaseEntityAbstract
    {
        public Guid? GIUD { get; set; }

        public int? ParameterId { get; set; }

        public string ParameterCategory { get; set; }

        public string ParameterType { get; set; }

        public string StorageType { get; set; }

        public virtual DocumentEntity Document { get; set; }

        public virtual List<CategoryEntity> ParameterCategories { get; set; } = new List<CategoryEntity>();

        public virtual List<ElementEntity> Elements { get; set; } = new List<ElementEntity>();

        public virtual List<ElementTypeEntity> Types { get; set; } = new List<ElementTypeEntity>();
    }
}
