using Serilog;
using ZhoBotDiscord;

namespace AppConfigurator
{
    internal class Program
    {
        private readonly IServiceProvider _provider;

        public Program()
        {
            Log.Logger = LoggerConfigurator.GetLogger();
            _provider = ServicesConfigurator.GetServiceProvider();
        }

        public static Task Main() => new Program().MainAsync();

        private async Task MainAsync()
        {
            var botRunner = new BotRunner(_provider);
            await botRunner.RunBotAppAsync();
        }
    }
}