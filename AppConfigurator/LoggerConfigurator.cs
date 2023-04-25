using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace AppConfigurator
{
    internal static class LoggerConfigurator
    {
        private const string PathToLog = "Logs/log-.txt";

        public static Logger GetLogger()
        {
            using var logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(new RenderedCompactJsonFormatter(), PathToLog, rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Warning)
                .CreateLogger();

            return logger;
        }
    }
}