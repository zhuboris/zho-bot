using DownloadAPI.Files;
using DownloadUtilsApi.DependencyInjection.ResponceHandlers;
using DownloadUtilsApi.YtDlp;

namespace DownloadAPI.Downloaders.VideoOnly
{
    internal sealed class YtDlpVideoDownloader : FileDownloader
    {
        private readonly IDownloaderResponceHandler _downloaderResponceHandler;
        private readonly IRecoderResponceHandler _recoderResponceHandler;

        public YtDlpVideoDownloader(FileData fileToDownload, FileStorage storage, IDownloaderResponceHandler downloaderResponceHandler, IRecoderResponceHandler recoderResponceHandler) : base(fileToDownload, storage, SupportedTypes.Video)
        {
            _downloaderResponceHandler = downloaderResponceHandler;
            _recoderResponceHandler = recoderResponceHandler;
        }

        protected override async Task<(DownloadResult result, long? sizeInBytes)> TryGetSizeFromApiAsync()
        {
            var (result, sizeInBytes) = await _downloaderResponceHandler.TryGetSizeAsync(FileToDownload.InputedUrl, FileToDownload.PathWithoutExtension, FileToDownload.MaxSize);

            DownloadResult downloadingResult = result.ToDownloadResult();

            return (downloadingResult, sizeInBytes);
        }

        protected override async Task SetFilePathWithExtentionAsync()
        {
            string extension = await GetExtensionAsync();
            FileToDownload.SetPathWithExtension(extension);
        }

        private async Task<string> GetExtensionAsync()
        {
            return await _downloaderResponceHandler.GetExtensionAsync(FileToDownload.InputedUrl, FileToDownload.PathWithoutExtension, FileToDownload.MaxSize) ?? DefaultExtension;
        }

        protected override async Task<DownloadResult> DownloadAsync()
        {
            YtDlpResult result;

            if (FileToDownload.ShouldSkipSizeCheck)
                result = await _downloaderResponceHandler.TryDownloadWithoutSizeCheckAsync(FileToDownload.InputedUrl, FileToDownload.PathWithoutExtension);
            else
                result = await _downloaderResponceHandler.TryDownloadAsync(FileToDownload.InputedUrl, FileToDownload.PathWithoutExtension, FileToDownload.MaxSize);

            return result.ToDownloadResult();
        }

        protected override Task<string> GetCorrectExtensionAsync()
        {
            return _recoderResponceHandler.RecodeVideoIfNeededAsync(FileToDownload.PathWithExtension);
        }
    }
}