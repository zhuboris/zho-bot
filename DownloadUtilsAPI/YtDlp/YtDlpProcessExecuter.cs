using DownloadUtilsApi.DependencyInjection.ProcessExecuters;
using DownloadUtilsApi.Utils;
using GlobalUtils;

namespace DownloadUtilsApi.YtDlp
{
    internal class YtDlpProcessExecuter : IDownloaderProcessExecuter
    {
        public Task<(string errors, string output)> GetSizeAsync(string url, string path, long maxSize)
        {
            string command = GetCommandToGetSize(url, path, maxSize);
            return ProcessUtils.ExecuteProcessWithOutputAsync(Paths.YtDlp, command);
        }

        public Task<(string errors, string output)> GetFilenameAsync(string url, string path, long maxSize)
        {
            string command = GetCommandToGetFilename(url, path, maxSize);
            return ProcessUtils.ExecuteProcessWithOutputAsync(Paths.YtDlp, command);            
        }

        public Task<string> DownloadAsync(string url, string path, long maxSize)
        {
            string command = GetCommandToDownload(url, path, maxSize);
            return ProcessUtils.ExecuteProcessAsync(Paths.YtDlp, command);
        }

        public Task<string> DownloadAnySizeAsync(string url, string path)
        {
            string command = GetCommandToDownloadAnySize(url, path);
            return ProcessUtils.ExecuteProcessAsync(Paths.YtDlp, command);            
        }

        private string GetCommandToGetSize(string url, string path, long maxSize)
        {
            string options = $"{Options.GetPath(path)} {Options.GetMaxNumberOfVideos()} {Options.GetFormat(maxSize)} {Options.GetSize()}";
            return $"\"{url}\" {options}";
        }

        private string GetCommandToGetFilename(string url, string path, long maxSize)
        {
            string options = $"{Options.GetPath(path)} {Options.GetMaxNumberOfVideos()} {Options.GetFormat(maxSize)} {Options.GetFileName()}";
            return $"\"{url}\" {options}";
        }

        private string GetCommandToDownload(string url, string path, long maxSize)
        {
            string options = $"{Options.GetPath(path)} {Options.GetMaxNumberOfVideos()} {Options.GetFormat(maxSize)}";
            return $"\"{url}\" {options}";
        }

        private string GetCommandToDownloadAnySize(string url, string path)
        {
            string options = $"{Options.GetPath(path)} {Options.GetMaxNumberOfVideos()}";
            return $"\"{url}\" {options}";
        }
    }
}