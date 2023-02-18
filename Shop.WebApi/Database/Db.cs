using Shop.WebApi.Articles.Models;
using System.Collections.Generic;

namespace Shop.WebApi.Database
{
    public interface IDb
    {
        IList<ArticleDbEntity> Articles { get; }
    }

    public class Db : IDb
    {
        public IList<ArticleDbEntity> Articles { get; } = new List<ArticleDbEntity>();
    }
}
