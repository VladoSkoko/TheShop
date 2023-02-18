using System.Linq;
using Shop.WebApi.Models;

namespace Shop.WebApi.Services
{
    public interface IArticleRepository
    {
        ArticleDbEntity GetById(int id);
        void Insert(ArticleDbEntity article);
    }
    public class ArticleRepository : IArticleRepository
    {
        private IDb Db { get; }
        public ArticleRepository(IDb Db) 
        {
            this.Db = Db;
        }

        public ArticleDbEntity GetById(int id)
        {
            return this.Db.Articles.SingleOrDefault(x => x.Id == id);
        }

        public void Insert(ArticleDbEntity article)
        {
            this.Db.Articles.Add(article);
        }
    }
}