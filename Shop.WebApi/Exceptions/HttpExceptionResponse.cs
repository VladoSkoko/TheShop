using Shop.WebApi.Exceptions.Types;
using System;
using System.Net;

namespace Shop.WebApi.Exceptions
{
    public class HttpExceptionResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public Exception Exception { get; set; }

        public HttpExceptionResponse(Exception exception) 
        {
            if (exception is HttpStatusCodeException)
            {
                this.StatusCode = (exception as HttpStatusCodeException).StatusCode;
                this.Exception = exception;
            }
            else
            {
                var internalServerException = new InternalServerException(exception);

                this.StatusCode = HttpStatusCode.InternalServerError;
                this.Exception = internalServerException;
            }
        }
    }
}
