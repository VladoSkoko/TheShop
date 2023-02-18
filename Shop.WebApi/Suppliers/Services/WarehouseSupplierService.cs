using System;
using System.Threading.Tasks;
using Shop.WebApi.Articles.Models;

namespace Shop.WebApi.Suppliers.Services
{
    public class WarehouseSupplierService : ISupplierService
    {
        public Task<bool> ArticleInInventoryAsync(int id)
        {
            return Task.FromResult(new Random().NextDouble() >= 0.5);
        }

        public Task<ArticleDto> GetArticleAsync(int id)
        {
            return Task.FromResult(
                new ArticleDto()
                {
                    Name = $"Article {id}",
                    Price = new Random().Next(100, 500),
                    IsSold = false,
                }
            );
        }
    }
}