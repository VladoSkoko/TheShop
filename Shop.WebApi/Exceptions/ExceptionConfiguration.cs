using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Shop.WebApi.Exceptions
{
    public class ExceptionConfiguration
    {
        public static void ConfigureFilters(HttpConfiguration config)
        {
            config.Filters.Add(new GlobalExceptionToResponseFilterAttribute());
        }
    }
}
