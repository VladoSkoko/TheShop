using Shop.WebApi.Localisation;
using System;

namespace Shop.WebApi.Exceptions
{
    public class InternalServerException : Exception
    {
        public InternalServerException(Exception innerException)
            : base(
                  string.Format(
                      Translator.TranslateExceptionMessage(ExceptionMessage.InternalServerError), Guid.NewGuid().ToString()
                  ),
                  innerException
            )
        {
        }
    }
}
