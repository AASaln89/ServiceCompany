using ServiceCompany.DbStuff.Models;

namespace ManagementCompany.DbStuff.ExternalEntities
{
    public class CategoryEntity : BaseEntityAbstract
    {
        public int CategoryId { get; set; }

        public string CategoryType { get; set; }

        public virtual DocumentEntity Document { get; set; }

        public virtual List<ParameterEntity> CategoryParameters { get; set; } = new List<ParameterEntity>();

        public virtual List<ElementEntity> Elements { get; set; } = new List<ElementEntity>();

        public virtual List<ElementTypeEntity> ElementTypes { get; set; } = new List<ElementTypeEntity>();

        public CategoryEntity() : base() { }
    }
}
