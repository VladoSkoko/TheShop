using System.Configuration;

namespace Shop.WebApi.Configuration
{
    public interface IAppConfiguration
    {
        string GetDealer1Url();
        string GetDealer2Url();
    }

    public class AppConfiguration : IAppConfiguration
    {
        public string GetDealer1Url()
        {
            return ConfigurationManager.AppSettings["Dealer1Url"];
        }

        public string GetDealer2Url()
        {
            return ConfigurationManager.AppSettings["Dealer2Url"];
        }
    }
}
