using Microsoft.EntityFrameworkCore;
using ServiceCompany.Models;
using ServiceCompany.DbStuff.Models;
using ServiceCompany.DbStuff.Models.Enums;

namespace ServiceCompany.DbStuff.Repositories
{
    public class ArticleRepository : BaseRepository<Article>
    {
        public ArticleRepository(ServiceCompanyDbContext context) : base(context) { }

        public Article GetWithAuthorById(int id)
        {
            return _entities
                .Include(x => x.Author)
                .SingleOrDefault(ent => ent.Id == id);
        }

        public IEnumerable<Article> GetAllWithAuthor()
        {
            return _entities
                .Include(x => x.Author)
                .OrderBy(x => x.CreationDate)
                .ToList();
        }

        public void UpdateArticle(ArticleViewModel model, int id)
        {
            var article = _context.Articles.SingleOrDefault(x => x.Id == id);

            article.Title = model.Title;
            article.Description = model.Description;

            _context.SaveChanges();
        }

        public IEnumerable<Article> GetArticles()
        {
            return _entities
                .Include(x => x.Author)
                .ToList();
        }

        public int? AddLike(int articleId)
        {
            var article = GetById(articleId);

                article.ThumbsUp++;
                _context.SaveChanges();

                return article.ThumbsUp;
        }

        internal int? AddDislike(int articleId)
        {
            var article = GetById(articleId);

            article.ThumbsDown++;
            _context.SaveChanges();

            return article.ThumbsDown;
        }
    }
}
