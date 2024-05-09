using ServiceCompanyChatService.DbStuff.Models;

namespace ServiceCompany.DbStuff.Models
{
    public class Article : BaseModelAbstract
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual User Author { get; set; }

        public int? ThumbsUp { get; set; }

        public int? ThumbsDown { get; set; }

        public virtual List<User>? Watchers { get; set; }

        public virtual List<Comment>? Comments { get; set; }

        public Article() : base() { }
    }
}
