using System.ComponentModel.DataAnnotations;

namespace ServiceCompany.Models
{
    public class LoginViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
