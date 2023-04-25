using DownloadAPI.DependencyInjection.Handlers;
using DownloadAPI.Files;
using DownloadUtilsApi.DependencyInjection.ResponceHandlers;
using GlobalUtils;

namespace DownloadAPI.Handlers
{
    public class ResponceHandler : IResponceHandler
    {
        private readonly FileStorage _storage;
        private readonly DownloaderHandler _downloaderHandler;

        public ResponceHandler(IRecoderResponceHandler recoderResponceHandler, IDownloaderResponceHandler downloaderResponceHandler)
        {
            _storage =  new(Paths.VideoFolder);
            _downloaderHandler = new VideoDownloaderHandler(recoderResponceHandler, downloaderResponceHandler);
        }

        public Task<(string errorText, string filePath)> SendResponceToDownloadVideoAsync(string? url, long uploadLimitInBytes)
        {
            var videoType = SupportedTypes.Video;
            return ResponceHandlerUtils.GetResponceToDownloadAsync(url, uploadLimitInBytes, _storage, videoType, _downloaderHandler.TryDownloadAsync);
        }

        public Task<(string errorText, string filePath)> SendResponceToDownloadGifAsync(string? url, long uploadLimitInBytes)
        {
            var gifType = SupportedTypes.Gif;
            return ResponceHandlerUtils.GetResponceToDownloadAsync(url, uploadLimitInBytes, _storage, gifType, _downloaderHandler.TryDownloadAsync);
        }

        public void SendResponceToDelete(string filePath)
        {
            _storage.DeleteAllFilesByName(filePath);
        }
    }

    internal static class ResponceHandlerUtils
    {
        public static async Task<(string errorText, string filePath)> GetResponceToDownloadAsync(string? url, long uploadLimitInBytes, FileStorage storage, SupportedTypes type, Func<FileData, FileStorage, SupportedTypes, Task<DownloadResult>> downloadTask)
        {
            string errorText = String.Empty;
            string filePath = String.Empty;

            if (IsUrlVaild(url) == false)
            {
                errorText = ErrorHandler.GetErrorMessage(DownloadResult.InvalidInput);
                return (errorText, filePath);
            }

            FileData file = FileBuilder.CreateFile(url, storage.FolderPath, uploadLimitInBytes);

            DownloadResult result = await Task.Run(() => downloadTask(file, storage, type));

            if (result.IsBadResult())
            {
                errorText = ErrorHandler.GetErrorMessage(result, uploadLimitInBytes);
                return (errorText, file.PathWithoutExtension);
            }

            return (errorText, file.PathWithExtension);
        }

        private static bool IsUrlVaild(string? url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri? uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }
    }
}