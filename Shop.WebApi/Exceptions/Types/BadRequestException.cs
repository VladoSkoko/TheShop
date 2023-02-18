using Shop.WebApi.Exceptions.Types;
using System.Net;

namespace Shop.WebApi.Exceptions
{
    public class BadRequestException : HttpStatusCodeException
    {
        public BadRequestException(ExceptionMessage message)
            : base(HttpStatusCode.BadRequest, message)
        {
        }
    }
}
