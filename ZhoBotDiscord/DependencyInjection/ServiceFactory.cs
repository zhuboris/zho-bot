using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace ZhoBotDiscord.DependencyInjection
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddDiscordBotServices(this IServiceCollection services)
        {
            var socketConfig = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.GuildMessages | GatewayIntents.DirectMessages | GatewayIntents.Guilds
            };

            var interactionConfig = new InteractionServiceConfig
            {
                DefaultRunMode = RunMode.Async,
            };

            return services
                .AddSingleton(new DiscordShardedClient(socketConfig))
                .AddSingleton(provider => new InteractionService(provider.GetRequiredService<DiscordShardedClient>(), interactionConfig));
        }
    }
}