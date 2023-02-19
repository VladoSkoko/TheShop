using System.Linq;
using Shop.WebApi.Articles.Models;
using Shop.WebApi.Database;

namespace Shop.WebApi.Articles.Repositories
{
    public interface IArticleRepository
    {
        ArticleDbEntity GetById(int id);
        void Insert(ArticleDbEntity article);
    }
    public class ArticleRepository : IArticleRepository
    {
        private readonly IDb Db;
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