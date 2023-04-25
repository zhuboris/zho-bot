using DownloadUtilsApi.YtDlp;
using GlobalUtils;

namespace DownloadAPI
{
    internal static class YtDlpResultExtensions
    {
        public static DownloadResult ToDownloadResult(this YtDlpResult result)
        {
            return result switch
            {
                YtDlpResult.Ok => DownloadResult.Ok,
                YtDlpResult.FormatNotAvaiable => DownloadResult.FormatNotAvaiable,
                YtDlpResult.OtherError => DownloadResult.InvalidInput,
                _ => throw new ArgumentException(MessagesText.Errors.UnexpectedType)
            };
        }
    }
}