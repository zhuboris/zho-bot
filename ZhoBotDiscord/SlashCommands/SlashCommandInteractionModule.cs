using Discord.Interactions;
using Discord.WebSocket;

namespace ZhoBotDiscord.SlashCommands
{
    public abstract class SlashCommandInteractionModule : InteractionModuleBase<ShardedInteractionContext>
    {
        private const string UnknownCommand = "Unknown command";
        private const string DirectMessage = "DM";

        protected string CommandName => Context.Interaction is SocketSlashCommand command 
            ? command.CommandName 
            : UnknownCommand;
        protected string UserFullName => $"{Context.User.Username}#{Context.User.Discriminator}";
        protected string GuildName => Context.Interaction.IsDMInteraction 
            ? DirectMessage 
            : Context.Guild.Name;
    }
}