using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using GlobalUtils;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ZhoBotDiscord.Handlers;

namespace ZhoBotDiscord
{
    internal class Initializer
    {
        private const string JsonPath = "discordtoken.json";
        private const string TokenKey = "DiscordBotToken";

        private readonly DiscordShardedClient _client;
        private readonly InteractionService _servise;
        private readonly IServiceProvider _provider;

        public Initializer(IServiceProvider provider)
        {
            _provider = provider;
            _client = _provider.GetRequiredService<DiscordShardedClient>();
            _servise = _provider.GetRequiredService<InteractionService>();

            EventHandlers.Init(_provider);
        }

        public async Task StartBotAsync()
        {            
            var token = TokenGetter.GetToken(JsonPath, TokenKey);

            if (String.IsNullOrWhiteSpace(token))
            {
                HandleInvalidToken();
            }

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            Log.Information(MessagesText.Formats.DiscordLogTemplate, nameof(Initializer), MessagesText.Info.SuccessfulInitialization);
        }

        public void SubscribeToDiscordApiEvents()
        {
            _client.Log += Logger.LogAsync;
            _servise.Log += Logger.LogAsync;
            _client.ShardReady += EventHandlers.OnShardReadyAsync;
            _client.InteractionCreated += EventHandlers.OnInteractionCreatedAsync;
        }

        public void UnsubscribeToDiscordApiEvents()
        {
            _client.Log -= Logger.LogAsync;
            _servise.Log -= Logger.LogAsync;
            _client.ShardReady -= EventHandlers.OnShardReadyAsync;
            _client.InteractionCreated -= EventHandlers.OnInteractionCreatedAsync;
        }

        private static void HandleInvalidToken()
        {
            Log.Fatal(MessagesText.Formats.DiscordLogTemplate, nameof(Initializer), MessagesText.Errors.TokenIsNullOrEmpty);
            throw new ArgumentException(MessagesText.Errors.TokenIsNullOrEmpty);
        }
    }
}