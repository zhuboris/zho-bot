using DownloadAPI.Downloaders;
using DownloadAPI.Files;
using DownloadUtilsApi.DependencyInjection.ResponceHandlers;

namespace DownloadAPI.Handlers
{
    internal class DownloaderHandler
    {
        protected readonly IRecoderResponceHandler RecoderResponceHandler;
        protected readonly IDownloaderResponceHandler DownloaderResponceHandler;
                
        public DownloaderHandler(IRecoderResponceHandler recoderResponceHandler, IDownloaderResponceHandler downloaderResponceHandler)
        {
            RecoderResponceHandler = recoderResponceHandler;
            DownloaderResponceHandler = downloaderResponceHandler;
        }

        public virtual async Task<DownloadResult> TryDownloadAsync(FileData file, FileStorage storage, SupportedTypes typeToDownload)
        {
            DownloadResult httpClientResult = await TryDownloadWithHttpClientAsync(file, storage, typeToDownload);
            return httpClientResult;
        }

        protected async Task<DownloadResult> TryDownloadWithHttpClientAsync(FileData file, FileStorage storage, SupportedTypes type)
        {
            using var httpClientDownloader = new HttpClientDownloader(file, storage, type, RecoderResponceHandler);
            return await httpClientDownloader.TrySetupFileAsync();
        }
    }
}