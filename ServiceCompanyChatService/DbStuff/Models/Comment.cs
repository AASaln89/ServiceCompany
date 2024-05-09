namespace ServiceCompanyChatService.DbStuff.Models
{
    public class Comment : BaseModel
    {
        public int UserId { get; set; }

        public int ArticleId { get; set; }

        public string UserName { get; set; }

        public string CommentMessage { get; set; }

        public Comment() : base() { }
    }
}
