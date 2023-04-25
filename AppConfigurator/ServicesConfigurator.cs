using DownloadAPI.DependencyInjection;
using DownloadUtilsApi.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ZhoBotDiscord.DependencyInjection;

namespace AppConfigurator
{
    internal static class ServicesConfigurator
    {
        public static IServiceProvider GetServiceProvider()
        {
            return new ServiceCollection()
                .AddDownloadUtilsServices()
                .AddDownloadServices()
                .AddDiscordBotServices()
                .BuildServiceProvider();
        }
    }
}