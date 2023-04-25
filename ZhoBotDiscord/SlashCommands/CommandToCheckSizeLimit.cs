using Discord.Interactions;
using GlobalUtils;
using ZhoBotDiscord.Utils;

namespace ZhoBotDiscord.SlashCommands
{
    public sealed class CommandToCheckSizeLimit : InteractionModuleBase<ShardedInteractionContext>
    {

        [SlashCommand(CommandsNames.GetFilesLimit.Name, CommandsNames.GetFilesLimit.EnDescription)]
        public async Task SendLimitValueAsync()
        {
            float limitInMb = Context.Guild.MaxUploadLimit.ConvertToMb();
            string message = String.Format(MessagesText.Formats.SizeLimitGetter, (int)limitInMb);

            await RespondAsync(text: message, ephemeral: true);
        }
    }
}