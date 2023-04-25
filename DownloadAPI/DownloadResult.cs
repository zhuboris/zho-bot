namespace DownloadAPI
{
    internal enum DownloadResult
    {
        Ok,
        ResultIsNull,
        ResponceError,
        InvalidInput,
        IncorrectFileType,
        FormatNotAvaiable,
        SizeLimitExceeded,
        PathNotExist,
        QueueIsFull,
    }

    internal static class DownloadResultExtensions
    {
        public static bool IsBadResult(this DownloadResult result)
        {
            return result != DownloadResult.Ok;
        }

        public static DownloadResult ConvertToNotNullable(this DownloadResult? result)
        {
            return result is not null 
                ? (DownloadResult)result 
                : DownloadResult.ResultIsNull;
        }
    }
}