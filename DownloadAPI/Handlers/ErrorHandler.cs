using GlobalUtils;

namespace DownloadAPI.Handlers
{
    internal static class ErrorHandler
    {
        public static string GetErrorMessage(DownloadResult result, long sizeLimit = 0)
        {
            float sizeLimitInMb = sizeLimit.ConvertToMb();

            return result switch
            {
                DownloadResult.InvalidInput => MessagesText.Errors.WrongInput,
                DownloadResult.ResponceError => MessagesText.Errors.ConnectionError,
                DownloadResult.IncorrectFileType => MessagesText.Errors.WrongType,
                DownloadResult.SizeLimitExceeded => String.Format(MessagesText.Formats.SizeTooBigFormat, (int)sizeLimitInMb),
                DownloadResult.ResultIsNull or DownloadResult.PathNotExist => MessagesText.Errors.DownloadError,
                DownloadResult.QueueIsFull => MessagesText.Errors.QueueIsFull,
                DownloadResult.FormatNotAvaiable => String.Format(MessagesText.Formats.NoFormatsAviable, (int)sizeLimitInMb),
                _ => string.Empty,
            };
        }
    }
}