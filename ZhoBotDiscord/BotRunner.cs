namespace ZhoBotDiscord
{
    public class BotRunner 
    {
        private readonly Initializer _initializer;

        public BotRunner(IServiceProvider serviceProvider)
        {
            _initializer = new(serviceProvider);
        }

        public async Task RunBotAppAsync()
        {
            _initializer.SubscribeToDiscordApiEvents();

            await _initializer.StartBotAsync();
            await Task.Delay(Timeout.Infinite);
            _initializer.UnsubscribeToDiscordApiEvents();
        }
    }
}