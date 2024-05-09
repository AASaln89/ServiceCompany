namespace ServiceCompany.Models
{
    public class BlogViewModel : BaseViewModel
    {
        public string? UserNickName { get; set; }

        public List<ArticleViewModel> Articles { get; set; }
    }
}
