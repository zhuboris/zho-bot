using Discord;
using GlobalUtils;
using Serilog;
using Serilog.Events;

namespace ZhoBotDiscord
{
    internal static class Logger
    {
        public static Task LogAsync(LogMessage message)
        {
            LogEventLevel severity = message.Severity.ToSerilogLogEventLevel();
            Log.Write(severity, message.Exception, MessagesText.Formats.DiscordLogTemplate, message.Source, message.Message);
            return Task.CompletedTask;
        }

        private static LogEventLevel ToSerilogLogEventLevel(this LogSeverity message)
        {
            return message switch
            {
                LogSeverity.Critical => LogEventLevel.Fatal,
                LogSeverity.Error => LogEventLevel.Error,
                LogSeverity.Warning => LogEventLevel.Warning,
                LogSeverity.Info => LogEventLevel.Information,
                LogSeverity.Verbose => LogEventLevel.Verbose,
                LogSeverity.Debug => LogEventLevel.Debug,
                _ => LogEventLevel.Information
            };
        }
    }
}