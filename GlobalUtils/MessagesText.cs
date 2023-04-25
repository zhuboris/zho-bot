namespace GlobalUtils
{
    public static class MessagesText
    {
        public static class Errors
        {
            public const string Error = "ERROR";
            public const string FormatNotAvaiable = "Requested format is not available";
            public const string UnexpectedType = "Unexpected Type";
            public const string PathMustBeSet = "Path must be set.";
            public const string DidNotInit = "Initialization has not been performed";

            public const string WrongInput = "Input does not contain URL or source not supported.";
            public const string WrongType = "URL does not contain suitable file to download.";
            public const string ConnectionError = "Connection error, or file cannot be downloaded from this source.";
            public const string DownloadError = "Download Error.";
            public const string QueueIsFull = "The server received too many requests, please try again later.";
            public const string UploadFail = "File was downloaded, but exceeded the download limit of your server.";
            public const string TokenIsNullOrEmpty = "Token is null or empty.";
        }

        public static class Info
        {
            public const string SuccessfulDownload = "Successful download";
            public const string SuccessfulInitialization = "Initialized";
            public const string QueueIsFull = "Queue is full";
            public const string StorageIsFull = "Storage is full";
        }

        public static class Formats
        {
            public const string NoFormatsAviable = "URL does not contain suitable file to download or the file is too big (Maximum size is {0} mb).";
            public const string SizeTooBigFormat = "File is too big. Maximum size is {0} mb.";
            public const string SizeLimitGetter = "It is possiple to upload up to {0} Mb attachments in one message on this server.";
            public const string UsersQuote = "\n>>> {0}";
            public const string SearchNamePattern = "{0}*";
            public const string WindowsCmdFormat = "/C chcp 65001 >nul 2>&1 && \"{0}\" {1}";
            public const string DiscordLogTemplate = "[{Source}] {Message}";
            public const string SlashCommandStartTemplate = "[{CommandName}] Was used by {User} from {Guild}";
            public const string SlashCommandToDownloadLogStartTemplate = "[{CommandName}] Was used by {User} from {Guild} to download {Url}";
            public const string SlashCommandToDownloadLogResultTemplate = "[{CommandName}] Used by {User} from {Guild} to download {Url} executed with {Result}";
        }
    }
}