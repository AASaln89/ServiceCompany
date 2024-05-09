namespace ServiceCompany.Models
{
    public class ChatViewModel : BaseViewModel
    {
        public string? UserNickName { get; set; }

        public List<ArticleViewModel> Articles { get; set; }
    }
}
