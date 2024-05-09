using ServiceCompany.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace ServiceCompany.Models
{
    public class RegistrationViewModel : BaseViewModel
    {
        public int Id { get; set; }

        //[RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Поле должно содержать @ и .")]
        [Required]
        public string Email { get; set; }

        //[MaxLength(20, ErrorMessage = "Максимальная длина 20 символов")]
        [Required]
        public string NickName { get; set; }

        //[CheckPasswordRegView]
        [Required]
        public string Password { get; set; }
    }
}
