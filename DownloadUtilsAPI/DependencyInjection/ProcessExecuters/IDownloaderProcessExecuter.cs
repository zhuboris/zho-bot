namespace DownloadUtilsApi.DependencyInjection.ProcessExecuters
{
    public interface IDownloaderProcessExecuter
    {
        public Task<(string errors, string output)> GetSizeAsync(string url, string path, long maxSize);

        public Task<(string errors, string output)> GetFilenameAsync(string url, string path, long maxSize);

        public Task<string> DownloadAsync(string url, string path, long maxSize);

        public Task<string> DownloadAnySizeAsync(string url, string path);
    }
}