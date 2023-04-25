using Discord.Interactions;
using DownloadAPI.DependencyInjection.Handlers;
using ZhoBotDiscord.Utils;

namespace ZhoBotDiscord.SlashCommands
{
    public sealed class CommandsToUploadAttachments : AttachmentUploaderModule
    {
        public CommandsToUploadAttachments(IResponceHandler responceHandler) : base(responceHandler) { }

        [SlashCommand(CommandsNames.PostVideo.Name, CommandsNames.PostVideo.EnDescription)]
        public async Task PostVideoAsync([Summary(CommandsNames.Url.Name, CommandsNames.Url.EnDescription)] string url,
            [Summary(CommandsNames.Messasge.Name, CommandsNames.Messasge.EnDescription)] string message = "",
            [Summary(CommandsNames.AttachmentName.Name, CommandsNames.AttachmentName.EnDescription)] string videoName = "")
        {
            await PostAsync(url, message, videoName, ResponceHandler.SendResponceToDownloadVideoAsync);
        }

        [SlashCommand(CommandsNames.PostGif.Name, CommandsNames.PostGif.EnDescription)]
        public async Task PostGifAsync([Summary(CommandsNames.Url.Name, CommandsNames.Url.EnDescription)] string url,
            [Summary(CommandsNames.Messasge.Name, CommandsNames.Messasge.EnDescription)] string message = "",
            [Summary(CommandsNames.AttachmentName.Name, CommandsNames.AttachmentName.EnDescription)] string gifName = "")
        {
            await PostAsync(url, message, gifName, ResponceHandler.SendResponceToDownloadGifAsync);
        }
    }
}