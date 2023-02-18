using Shop.WebApi.Exceptions;
using System.Reflection;
using System.Resources;

namespace Shop.WebApi.Localisation
{
    public class Translator
    {
        public static string TranslateExceptionMessage(ExceptionMessage message)
        {
            var resourceManager = new ResourceManager("Shop.WebApi.Resources.ExceptionMessages", Assembly.GetExecutingAssembly());

            return resourceManager.GetString(ExceptionDictionary.GetString(message));
        }
    }
}
