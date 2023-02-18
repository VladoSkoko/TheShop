using Shop.WebApi.Articles.Models;
using System.Threading.Tasks;

namespace Shop.WebApi.Suppliers.Services
{
    public interface ISupplierService
    {
        Task<bool> ArticleInInventoryAsync(int id);
        Task<ArticleDto> GetArticleAsync(int id);
    }
}
