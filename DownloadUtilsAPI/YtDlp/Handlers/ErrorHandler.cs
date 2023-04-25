using GlobalUtils;

namespace DownloadUtilsApi.YtDlp.Handlers
{
    internal static class ErrorHandler
    {
        public static YtDlpResult GetResult(string errorOutput)
        {
            if (String.IsNullOrWhiteSpace(errorOutput) || errorOutput.Contains(MessagesText.Errors.Error) == false)
                return YtDlpResult.Ok;

            if (errorOutput.Contains(MessagesText.Errors.FormatNotAvaiable, StringComparison.OrdinalIgnoreCase))
                return YtDlpResult.FormatNotAvaiable;

            return YtDlpResult.OtherError;
        }
    }
}