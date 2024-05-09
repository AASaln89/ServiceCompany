using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiceCompany.DbStuff.Models
{
    public class CompanyProfile : BaseModelAbstract
    {
        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public byte[]? Avatar { get; set; }

        public string? BankName { get; set; }

        public int? BankAccount { get; set; }

        public double FinBalance { get; set; }

        public virtual Company? Company { get; set; }
    }
}
