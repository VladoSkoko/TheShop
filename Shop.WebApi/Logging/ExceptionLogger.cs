using NLog;
using System;

namespace Shop.WebApi.Logging
{
    public class ExceptionLogger
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public static void Log(Exception exception)
        {
            _logger.Error(exception.ToString());
        }
    }
}
