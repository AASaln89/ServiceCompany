using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiceCompany.DbStuff.Models
{
    public class UserProfile : BaseModelAbstract
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? NickName { get; set; }

        public byte[]? Avatar { get; set; }

        public virtual User? User { get; set; }

        public UserProfile() : base() { }
    }
}
