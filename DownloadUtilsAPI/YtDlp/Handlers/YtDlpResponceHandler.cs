using DownloadUtilsApi.DependencyInjection.ProcessExecuters;
using DownloadUtilsApi.DependencyInjection.ResponceHandlers;

namespace DownloadUtilsApi.YtDlp.Handlers
{
    public class YtDlpResponceHandler : IDownloaderResponceHandler
    {
        private readonly IDownloaderProcessExecuter _downloaderProcessExecuter;

        public YtDlpResponceHandler(IDownloaderProcessExecuter downloaderProcessExecuter)
        {
            _downloaderProcessExecuter = downloaderProcessExecuter;
        }

        public async Task<(YtDlpResult result, long? size)> TryGetSizeAsync(string url, string path, long maxSize)
        {
            var (errors, output) = await _downloaderProcessExecuter.GetSizeAsync(url, path, maxSize); 

            YtDlpResult result = ErrorHandler.GetResult(errors);

            if (result != YtDlpResult.Ok)
                return (result, null);

            bool isParsed = long.TryParse(output, out long parsedSize);
            return isParsed 
                ? (YtDlpResult.Ok, parsedSize) 
                : (YtDlpResult.FormatNotAvaiable, null);
        }

        public async Task<string?> GetExtensionAsync(string url, string path, long maxSize)
        {
            var (errors, output) = await _downloaderProcessExecuter.GetFilenameAsync(url, path, maxSize);            

            YtDlpResult result = ErrorHandler.GetResult(errors);

            if (result != YtDlpResult.Ok)
                return null;

            output = output.Trim();
            return Path.GetExtension(output);
        }

        public async Task<YtDlpResult> TryDownloadAsync(string url, string path, long maxSize)
        {
            var errors = await _downloaderProcessExecuter.DownloadAsync(url, path, maxSize);            
            return ErrorHandler.GetResult(errors);
        }

        public async Task<YtDlpResult> TryDownloadWithoutSizeCheckAsync(string url, string path)
        {
            var errors = await _downloaderProcessExecuter.DownloadAnySizeAsync(url, path);
            
            return ErrorHandler.GetResult(errors);
        }
    }
}