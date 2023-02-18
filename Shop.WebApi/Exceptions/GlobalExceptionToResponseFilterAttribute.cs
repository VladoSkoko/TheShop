using System.Net.Http;
using System.Web.Http.Filters;
using Shop.WebApi.Logging;

namespace Shop.WebApi.Exceptions
{
    public class GlobalExceptionToResponseFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var exceptionResponse = new HttpExceptionResponse(context.Exception);

            ExceptionLogger.Log(exceptionResponse.Exception);

            context.Response = context.Request.CreateErrorResponse(exceptionResponse.StatusCode, exceptionResponse.Exception.Message);
        }
    }
}
