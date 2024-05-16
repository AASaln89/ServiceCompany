using ServiceCompany.DbStuff.Models;

namespace ServiceCompany.DbStuff.Postgres.Entities
{
    public class Test : BaseEntityAbstract
    {
        public string? NNam {  get; set; }

        public List<int>? NamIds { get; set; }
    }
}
