using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.WebApi.Logging
{
    public class LoggingConfiguration
    {
        public static void Configure()
        {
            NLog.LogManager.Setup().LoadConfiguration(
                (NLog.Config.ISetupLoadConfigurationBuilder builder) => {
                    var debugTarget = new FileTarget
                    {
                        FileName = "${basedir}/Logs/debug.log",
                        Layout = "Debug: ${message}"
                    };

                    var infoTarget = new FileTarget
                    {
                        FileName = "${basedir}/Logs/info.log",
                        Layout = "Info: ${message}"
                    };

                    var errorTarget = new FileTarget
                    {
                        FileName = "${basedir}/Logs/error.log",
                        Layout = "Error: ${message}"
                    };

                    builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteTo(debugTarget);
                    builder.ForLogger().FilterMinLevel(LogLevel.Info).WriteTo(infoTarget);
                    builder.ForLogger().FilterMinLevel(LogLevel.Error).WriteTo(errorTarget);
                });
        }
    }
}
