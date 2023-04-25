using DownloadUtilsApi.YtDlp;

namespace DownloadUtilsApi.DependencyInjection.ResponceHandlers
{
    public interface IDownloaderResponceHandler
    {
        public Task<(YtDlpResult result, long? size)> TryGetSizeAsync(string url, string path, long maxSize);

        public Task<string?> GetExtensionAsync(string url, string path, long maxSize);

        public Task<YtDlpResult> TryDownloadAsync(string url, string path, long maxSize);

        public Task<YtDlpResult> TryDownloadWithoutSizeCheckAsync(string url, string path);        
    }
}