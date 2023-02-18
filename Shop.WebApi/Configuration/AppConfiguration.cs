using Shop.WebApi.Exceptions;
using Shop.WebApi.Localisation;
using Shop.WebApi.Suppliers.Enums;
using System;
using System.Configuration;

namespace Shop.WebApi.Configuration
{
    public interface IAppConfiguration
    {
        string GetDealerUrl(Dealer dealer);
    }

    public class AppConfiguration : IAppConfiguration
    {
        public string GetDealerUrl(Dealer dealer)
        {
            switch (dealer)
            {
                case Dealer.Dealer1:
                    return ConfigurationManager.AppSettings["Dealer1Url"];
                case Dealer.Dealer2:
                    return ConfigurationManager.AppSettings["Dealer2Url"];
                default:
                    throw new NotSupportedException(
                        string.Format(Translator.TranslateExceptionMessage(ExceptionMessage.UnsupportedDealerProvided), dealer.ToString())
                    );
            }
        }
    }
}
