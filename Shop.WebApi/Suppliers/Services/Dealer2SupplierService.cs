using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shop.WebApi.Articles.Models;
using Shop.WebApi.Configuration;

namespace Shop.WebApi.Suppliers.Services
{
    public class Dealer2SupplierService : ISupplierService
    {
        private readonly IAppConfiguration appConfiguration;

        public Dealer2SupplierService(IAppConfiguration appConfiguration)
        {
            this.appConfiguration = appConfiguration;
        }

        public async Task<bool> ArticleInInventoryAsync(int id)
        {
            string supplierUrl = this.appConfiguration.GetDealer2Url();

            using (var client = new HttpClient())
            {
                var content = await client.GetStringAsync($"{supplierUrl}/ArticleInInventory/{id}");
                return JsonConvert.DeserializeObject<bool>(content);
            }
        }

        public async Task<ArticleDto> GetArticleAsync(int id)
        {
            string supplierUrl = this.appConfiguration.GetDealer2Url();

            using (var client = new HttpClient())
            {
                var content = await client.GetStringAsync($"{supplierUrl}/ArticleInInventory/{id}");
                return JsonConvert.DeserializeObject<ArticleDto>(content);
            }
        }
    }
}