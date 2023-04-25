using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using GlobalUtils;
using Serilog;
using ZhoBotDiscord.Utils;

namespace ZhoBotDiscord.SlashCommands
{
    public sealed class InfoCommands : SlashCommandInteractionModule
    {
        [SlashCommand(CommandsNames.ShowInfo.Name, CommandsNames.ShowInfo.EnDescription)]
        public async Task SendInfoAsync()
        {
            string userName = Context.User is SocketGuildUser user
                ? user.DisplayName 
                : Context.User.Username;
          string message = String.Format(Texts.Introduction, userName);
            
            ButtonBuilder showCommandsButton = SetStandartButton(CommandsNames.ShowInfo.ShowCommandsButtonLabel, Emotes.List, CommandsNames.ShowInfo.ShowCommandsButtonId);
            ButtonBuilder inviteButton = SetLinkButton(CommandsNames.ShowInfo.InviteButtonLabel, Emotes.Plus, Paths.InviteBotUrl);
            ButtonBuilder gitButton = SetLinkButton(CommandsNames.ShowInfo.GitLinkLabel, Emotes.Git, Paths.GitUrl);

            var component = new ComponentBuilder()
                .WithButton(showCommandsButton)
                .WithButton(inviteButton)
                .WithButton(gitButton);

            await RespondAsync(text: message, components: component.Build(), ephemeral: true);
            LogExecuting();
        }


        [SlashCommand(CommandsNames.ShowCommands.Name, CommandsNames.ShowCommands.EnDescription)]
        public async Task SendCommandsDescriptionAsync()
        {
            await RespondAsync(text: Texts.CommandsDescription, ephemeral: true);
            LogExecuting();
        }

        private void LogExecuting()
        {
            Log.Information(MessagesText.Formats.SlashCommandStartTemplate, CommandName, UserFullName, GuildName);
        }

        private ButtonBuilder SetLinkButton(string label, IEmote emote, string link)
        {
            return new ButtonBuilder()
                            .WithLabel(label)
                            .WithEmote(emote)
                            .WithStyle(ButtonStyle.Link)
                            .WithUrl(link);
        }

        private ButtonBuilder SetStandartButton(string label, IEmote emote, string id)
        {
            return new ButtonBuilder()
                            .WithLabel(label)
                            .WithEmote(emote)
                            .WithStyle(ButtonStyle.Secondary)
                            .WithCustomId(id);
        }
    }
}