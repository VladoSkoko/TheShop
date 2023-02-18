using Shop.WebApi.Localisation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.WebApi.Exceptions
{
    public class ExceptionDictionary
    {
        private static readonly Dictionary<ExceptionMessage, string> _exceptionMessages =
            Enum.GetValues(typeof(ExceptionMessage))
                .Cast<ExceptionMessage>()
                .ToDictionary(
                    (ExceptionMessage message) => message, message => message.ToString()
                );

        public static string GetString(ExceptionMessage message)
        {
            if (!_exceptionMessages.TryGetValue(message, out string value))
            {
                throw new KeyNotFoundException(Translator.TranslateExceptionMessage(ExceptionMessage.InvalidExceptionMessage));
            }

            return value;
        }
    }
}
