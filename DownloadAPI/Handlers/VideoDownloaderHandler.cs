using DownloadAPI.Downloaders.VideoOnly;
using DownloadAPI.Files;
using DownloadUtilsApi.DependencyInjection.ResponceHandlers;

namespace DownloadAPI.Handlers
{
    internal sealed class VideoDownloaderHandler : DownloaderHandler
    {
        public VideoDownloaderHandler(IRecoderResponceHandler recoderResponceHandler, IDownloaderResponceHandler downloaderResponceHandler) : base(recoderResponceHandler, downloaderResponceHandler) {}

        public override async Task<DownloadResult> TryDownloadAsync(FileData file, FileStorage storage, SupportedTypes typeToDownload = SupportedTypes.Video)
        {
            if (typeToDownload != SupportedTypes.Video)
                return await base.TryDownloadAsync(file, storage, typeToDownload);

            DownloadResult ytDlpResult = await TryDownloadWithYtDlpAsync(file, storage);

            if (ytDlpResult == DownloadResult.Ok)
                return ytDlpResult;

            DownloadResult httpClientResult = await TryDownloadWithHttpClientAsync(file, storage, typeToDownload);

            if (httpClientResult == DownloadResult.Ok)
                return httpClientResult;

            return GetBadResult(ytDlpResult, httpClientResult);
        }

        private Task<DownloadResult> TryDownloadWithYtDlpAsync(FileData file, FileStorage storage)
        {
            var ytDlpDownloader = new YtDlpVideoDownloader(file, storage, DownloaderResponceHandler, RecoderResponceHandler);
            return ytDlpDownloader.TrySetupFileAsync();
        }

        private DownloadResult GetBadResult(DownloadResult ytDlpResult, DownloadResult httpClientResult)
        {
            int ytDlpResultValue = (int)ytDlpResult;
            int httpClientResultValue = (int)httpClientResult;

            int moreImportantResultValue = Math.Max(ytDlpResultValue, httpClientResultValue);
            return (DownloadResult)moreImportantResultValue;
        }
    }
}