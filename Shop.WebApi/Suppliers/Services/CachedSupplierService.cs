using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.WebApi.Articles.Models;

namespace Shop.WebApi.Suppliers.Services
{
    public interface ICachedSupplierService : ISupplierService
    {
        void SetArticle(Article article);
    }

    public class CachedSupplierService : ICachedSupplierService
    {
        private readonly Dictionary<int, Article> _cachedArticles = new Dictionary<int, Article>();

        public Task<bool> ArticleInInventoryAsync(int id)
        {
            return Task.FromResult(_cachedArticles.ContainsKey(id));
        }

        public Task<ArticleDto> GetArticleAsync(int id)
        {
            _cachedArticles.TryGetValue(id, out Article article);
            return Task.FromResult(article.ToDto());
        }

        public void SetArticle(Article article)
        {
            _cachedArticles.Add(article.Id, article);
        }
    }
}