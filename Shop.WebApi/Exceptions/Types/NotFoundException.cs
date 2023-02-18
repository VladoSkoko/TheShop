using System.Net;

namespace Shop.WebApi.Exceptions.Types
{
    public class NotFoundException : HttpStatusCodeException
    {
        public NotFoundException(ExceptionMessage message)
            : base(HttpStatusCode.NotFound, message)
        {
        }
    }
}
