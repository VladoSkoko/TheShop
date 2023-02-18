using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shop.WebApi.Articles.Models;
using Shop.WebApi.Services;
using Shop.WebApi.Suppliers.Enums;

namespace Shop.WebApi.Suppliers.Services
{
    public class Dealer1SupplierService : ISupplierService
    {
        private IAppConfiguration appConfiguration;

        public Dealer1SupplierService(IAppConfiguration appConfiguration)
        {
            this.appConfiguration = appConfiguration;
        }

        public async Task<bool> ArticleInInventoryAsync(int id)
        {
            string supplierUrl = this.appConfiguration.GetDealerUrl(Dealer.Dealer1);

            using (var client = new HttpClient())
            {
                var content = await client.GetStringAsync($"{supplierUrl}/ArticleInInventory/{id}");
                return JsonConvert.DeserializeObject<bool>(content);
            }
        }

        public async Task<ArticleDto> GetArticleAsync(int id)
        {
            string supplierUrl = this.appConfiguration.GetDealerUrl(Dealer.Dealer1);

            using (var client = new HttpClient())
            {
                var content = await client.GetStringAsync($"{supplierUrl}/ArticleInInventory/{id}");
                return JsonConvert.DeserializeObject<ArticleDto>(content);
            }
        }
    }
}