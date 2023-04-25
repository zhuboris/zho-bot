using Discord;
using Discord.Interactions;
using DownloadAPI.DependencyInjection.Handlers;
using GlobalUtils;
using Serilog;
using Serilog.Events;

namespace ZhoBotDiscord.SlashCommands
{
    public abstract class AttachmentUploaderModule : SlashCommandInteractionModule
    {
        protected readonly IResponceHandler ResponceHandler;
        
        private string? _path;

        protected AttachmentUploaderModule(IResponceHandler responceHandler)
        {
            ResponceHandler = responceHandler;
        }

        public override void AfterExecute(ICommandInfo command)
        {
            if (String.IsNullOrEmpty(_path) == false)
                ResponceHandler.SendResponceToDelete(_path);
        }

        public async Task PostAsync(string url, string message, string videoName, Func<string, long, Task<(string errorText, string filePath)>> responce)
        {
            await DeferAsync(true);
            LogBeginingOfExecution(url);

            long uploadLimit = Context.Guild.MaxUploadLimit.ConvertToLong();
            var (errorText, filePath) = await Task.Run(() => responce(url, uploadLimit));

            _path = filePath;

            if (IsBadResult(errorText))
            {
                await FollowupAsync(errorText);
                LogExecutingResult(LogEventLevel.Warning, url, errorText);
                return;
            }

            await TrySendAttachmentAsync(message, videoName, filePath, url);
        }

        private async Task TrySendAttachmentAsync(string message, string fileName, string filePath, string url)
        {
            try
            {
                await SendAttachmentAsync(message, fileName, filePath);
                await FollowupAsync(MessagesText.Info.SuccessfulDownload);
                await Context.Interaction.DeleteOriginalResponseAsync();
                LogExecutingResult(LogEventLevel.Information, url, MessagesText.Info.SuccessfulDownload);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                await FollowupAsync(MessagesText.Errors.UploadFail);
                LogExecutingResult(LogEventLevel.Error, url, MessagesText.Errors.UploadFail, exception);
            }
        }

        private void LogBeginingOfExecution(string url)
        {
            Log.Information(MessagesText.Formats.SlashCommandToDownloadLogStartTemplate, CommandName, UserFullName, GuildName, url);
        }

        private void LogExecutingResult(LogEventLevel logEventLevel, string url, string message, Exception? exception = null)
        {
            Log.Write(logEventLevel, exception, MessagesText.Formats.SlashCommandToDownloadLogResultTemplate, CommandName, UserFullName, GuildName, url, message);
        }

        private Task SendAttachmentAsync(string message, string fileName, string filePath)
        {
            fileName = GetName(fileName, filePath);
            var formattedMessage = GetText(message);

            var video = new FileAttachment(path: filePath, fileName: fileName);
            return Context.Interaction.Channel.SendFileAsync(video, formattedMessage);
        }

        private string GetName(string videoName, string filePath)
        {
            if (String.IsNullOrEmpty(videoName))
                return Path.GetFileName(filePath);
            
            string extension = Path.GetExtension(filePath) ?? String.Empty;
            return $"{videoName}{extension}";
        }

        private bool IsBadResult(string errorText)
        {
            return String.IsNullOrWhiteSpace(errorText) == false;
        }

        private string GetText(string intputedMessage)
        {
            const string DefaultMessage = " posted:";

            var message = GetMessageFromUserIfItExists(intputedMessage);
            var text = $"{Context.User.Mention}{DefaultMessage}{message}";
            return text;
        }

        private string GetMessageFromUserIfItExists(string intputedMessage)
        {
            if (String.IsNullOrWhiteSpace(intputedMessage) != false)
                return String.Empty;

            return String.Format(MessagesText.Formats.UsersQuote, intputedMessage);
        }
    }
}