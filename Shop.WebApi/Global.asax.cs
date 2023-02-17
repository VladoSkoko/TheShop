using Shop.WebApi.DependencyInjection;
using Shop.WebApi.Logging;
using System.Web.Http;
using System.Web.Mvc;

namespace Shop.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            LoggingConfiguration.Configure();
            DependencyInjectionConfiguration.Configure(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
