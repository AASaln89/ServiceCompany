namespace ServiceCompany.DbStuff.Models
{
    public abstract class BaseEntityAbstract : BaseModelAbstract
    {
        public string Name { get; set; }

        public string UniqueId { get; set; }

        public BaseEntityAbstract() : base()
        {
            Name = "----";
            UniqueId = "----";
        }
    }
}
