using Shop.WebApi.Articles.Models;
using Shop.WebApi.Suppliers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.WebApi.Suppliers.Managers
{
    public interface ISupplierManager
    {
        Task<ArticleDto> GetArticleAsync(int id);
    }

    public class SupplierManager : ISupplierManager
    {
        private ICachedSupplierService cachedSupplierService;
        private IEnumerable<ISupplierService> externalSupplierServices;

        public SupplierManager(
            ICachedSupplierService cachedSupplierService,
            IEnumerable<ISupplierService> externalSupplierServices
        )
        {
            this.cachedSupplierService = cachedSupplierService;
            this.externalSupplierServices = externalSupplierServices;
        }

        public async Task<ArticleDto> GetArticleAsync(int id)
        {
            bool inCachedInventory = await this.cachedSupplierService.ArticleInInventoryAsync(id);

            if (inCachedInventory)
            {
                return await this.cachedSupplierService.GetArticleAsync(id);
            }

            bool[] inExternalInventory = await Task.WhenAll<bool>(
                this.externalSupplierServices.Select((ISupplierService supplierService) => supplierService.ArticleInInventoryAsync(id))
            );

            int externalInventoryIndex = Array.IndexOf<bool>(inExternalInventory, true);

            if (externalInventoryIndex != -1)
            {
                ArticleDto articleDto = await this.externalSupplierServices.ElementAt(externalInventoryIndex).GetArticleAsync(id);

                this.cachedSupplierService.SetArticle(new Article(id, articleDto));

                return articleDto;
            }

            return null;
        }
    }
}
