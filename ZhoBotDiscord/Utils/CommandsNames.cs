namespace ZhoBotDiscord.Utils
{
    internal static class CommandsNames
    {
        public const string RuLanguageCode = "ru";

        public static class ShowInfo
        {
            public const string Name = "info";
            public const string EnDescription = "Show info about bot.";
            public const string RuDescription = "Показать информацию о боте.";

            public const string InviteButtonLabel = "Add bot to your server";
            public const string ShowCommandsButtonLabel = "Show list of aviable commands";
            public const string ShowCommandsButtonId = "showCommands";
            public const string GitLinkLabel = "GitHub";

            public static readonly Dictionary<string, string> DescriptionLocalizations = new()
            {
                { RuLanguageCode, RuDescription }
            };
        }

        public static class ShowCommands
        {
            public const string Name = "help";
            public const string EnDescription = "Show list of aviable commands.";
            public const string RuDescription = "Показать список доступных комманд.";

            public static readonly Dictionary<string, string> DescriptionLocalizations = new()
            {
                { RuLanguageCode, RuDescription }
            };
        }

        public static class PostVideo
        {
            public const string Name = "post-video-by-url";
            public const string EnDescription = "Enter correct URL.";
            public const string RuDescription = "Введите подходящий URL.";

            public static readonly Dictionary<string, string> DescriptionLocalizations = new()
            {
                { RuLanguageCode, RuDescription }
            };
        }

        public static class PostGif
        {
            public const string Name = "post-gif-by-url";
            public const string EnDescription = "Enter correct URL.";
            public const string RuDescription = "Введите подходящий URL.";

            public static readonly Dictionary<string, string> DescriptionLocalizations = new()
            {
                { RuLanguageCode, RuDescription }
            };
        }

        public static class Url
        {
            public const string Name = "url";
            public const string EnDescription = "Enter correct URL.";
            public const string RuDescription = "Введите подходящий URL.";

            public static readonly Dictionary<string, string> DescriptionLocalizations = new()
            {
                { RuLanguageCode, RuDescription }
            };
        }

        public static class AttachmentName
        {
            public const string Name = "name-for-video";
            public const string EnDescription = "You can add name for video or it will be randomized.";
            public const string RuDescription = "Вы можете задать название для видео, или оно будет рандомным.";

            public static readonly Dictionary<string, string> DescriptionLocalizations = new()
            {
                { RuLanguageCode, RuDescription }
            };
        }

        public static class Messasge
        {
            public const string Name = "message";
            public const string EnDescription = "You can comment video.";
            public const string RuDescription = "Вы можете прокомментировать видео.";

            public static readonly Dictionary<string, string> DescriptionLocalizations = new()
            {
                { RuLanguageCode, RuDescription }
            };
        }

        public static class GetFilesLimit
        {
            public const string Name = "check-download-limit";
            public const string EnDescription = "It depends on server boost status.";
            public const string RuDescription = "Он зависит от уровня буста сервера.";

            public static readonly Dictionary<string, string> DescriptionLocalizations = new()
            {
                { RuLanguageCode, RuDescription }
            };
        }
    }
}
