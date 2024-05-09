namespace ServiceCompany.DbStuff.Models
{
    public abstract class BaseModelAbstract
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public Guid Guid { get; set; }

        public DateTime? CreationDate { get; set; }

        public BaseModelAbstract()
        {
            CreationDate = DateTime.Now;
            Guid = Guid.NewGuid();
            IsActive = true;
            Name = "---";
        }
    }
}
