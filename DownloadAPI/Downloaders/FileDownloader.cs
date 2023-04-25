using DownloadAPI.Files;

namespace DownloadAPI.Downloaders
{
    internal abstract class FileDownloader
    {
        protected readonly FileData FileToDownload;
        protected readonly FileStorage Storage;
        protected readonly string ExpectedMime;
        protected readonly string DefaultExtension;

        protected FileDownloader(FileData fileToDownload, FileStorage storage, SupportedTypes type)
        {
            FileToDownload = fileToDownload;
            Storage = storage;
            ExpectedMime = type.ExpectedMime();
            DefaultExtension = type.DefaultExtension();
        }

        protected abstract Task<(DownloadResult result, long? sizeInBytes)> TryGetSizeFromApiAsync();

        protected abstract Task SetFilePathWithExtentionAsync();

        protected abstract Task<DownloadResult> DownloadAsync();

        protected abstract Task<string> GetCorrectExtensionAsync();

        public virtual async Task<DownloadResult> TrySetupFileAsync()
        {
            var ifSizeFit = DownloadResult.Ok;

            if (FileToDownload.ShouldSkipSizeCheck == false)
                ifSizeFit = await CheckIfSizeFitAsync();

            if (ifSizeFit.IsBadResult())
                return ifSizeFit;

            await SetFilePathWithExtentionAsync();
            DownloadResult result = await DownloadAsync();

            if (result.IsBadResult())
                return result;

            string newExtension = await GetCorrectExtensionAsync();

            if (String.IsNullOrEmpty(newExtension) == false)
                FileToDownload.SetPathWithExtension(newExtension);

            var gotSize = FileToDownload.TryGetCurrentSize(out var size);

            if (gotSize == false)
                return DownloadResult.SizeLimitExceeded;

            Storage.AddSizeIfFileWasUntracked(FileToDownload, size);
            return size < FileToDownload.MaxSize 
                ? DownloadResult.Ok 
                : DownloadResult.SizeLimitExceeded;
        }

        private async Task<DownloadResult> CheckIfSizeFitAsync()
        {
            var (result, sizeInBytes) = await TryGetSizeFromApiAsync();

            if (result.IsBadResult())
                return result;

            if (sizeInBytes is null || sizeInBytes <= 0)
                return DownloadResult.ResponceError;

            if (sizeInBytes > FileToDownload.MaxSize)
                return DownloadResult.SizeLimitExceeded;

            DownloadResult isSizeEnouth = await Storage.WaitForDownloadQueueIfSizeFitAsync((long)sizeInBytes, FileToDownload);
            return isSizeEnouth;
        }
    }
}