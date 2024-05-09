using System.ComponentModel.DataAnnotations;

namespace ServiceCompany.Models.Alerts
{
    public class AlertViewModel : BaseViewModel
    {
        public int AlertId { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime? ExpireDate { get; set; }

        public List<AlertViewModel> LastAlerts { get; set; }
    }
}
