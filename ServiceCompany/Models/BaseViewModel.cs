namespace ServiceCompany.Models
{
    public class BaseViewModel
    {
        public string CurrentUserName { get; set; }

        public bool IsSuperAdmin { get; set; } = false;

        public bool IsAdmin { get; set; } = false;

        public bool IsUser { get; set; } = false;

        public bool IsGuest { get; set; } = false;

        public DateTime? CreationDate { get; set; }
    }
}
