using Shop.WebApi.Localisation;
using System;
using System.Net;

namespace Shop.WebApi.Exceptions.Types
{
    public abstract class HttpStatusCodeException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public HttpStatusCodeException(HttpStatusCode statusCode, ExceptionMessage message)
            : base(Translator.TranslateExceptionMessage(message))
        {
            this.StatusCode = statusCode;
        }
    }
}
