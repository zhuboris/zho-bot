using Discord.Interactions;
using Discord.WebSocket;
using GlobalUtils;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;
using ZhoBotDiscord.Utils;

namespace ZhoBotDiscord.Handlers
{
    internal static class EventHandlers
    {
        private static IServiceProvider? _provider;
        private static DiscordShardedClient? _client;
        private static InteractionService? _service;

        public static void Init(IServiceProvider provider)
        {
            _provider = provider;
            _client = provider.GetRequiredService<DiscordShardedClient>();
            _service = provider.GetRequiredService<InteractionService>();
        }

        public static async Task OnInteractionCreatedAsync(SocketInteraction interaction)
        {
            if (DidNotInit())
                HandleMissedInitialization(nameof(OnInteractionCreatedAsync));

            var context = new ShardedInteractionContext(_client, interaction);
            await _service.ExecuteCommandAsync(context, _provider);
            await TryRespondToButtonInteraction(interaction);
        }

        public static async Task OnShardReadyAsync(DiscordSocketClient shard)
        {
            if (DidNotInit())
                HandleMissedInitialization(nameof(OnShardReadyAsync));

            await _service.AddModulesAsync(Assembly.GetExecutingAssembly(), _provider);
            await _service.RegisterCommandsGloballyAsync();
        }

        private static bool DidNotInit()
        {
            return _provider is null || _client is null || _service is null;
        }

        private static void HandleMissedInitialization(string source)
        {
            Log.Fatal(MessagesText.Formats.DiscordLogTemplate, source, MessagesText.Errors.DidNotInit);
            throw new InvalidOperationException(MessagesText.Errors.DidNotInit);
        }

        private static Task TryRespondToButtonInteraction(SocketInteraction interaction)
        {
            return interaction is SocketMessageComponent componentInteraction && componentInteraction.Data.CustomId == CommandsNames.ShowInfo.ShowCommandsButtonId
                ? componentInteraction.RespondAsync(Texts.CommandsDescription, ephemeral: true)
                : Task.CompletedTask;
        }
    }
}
