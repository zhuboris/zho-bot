using DownloadAPI.Files;
using DownloadUtilsApi.DependencyInjection.ResponceHandlers;
using GlobalUtils;
using HeyRed.Mime;
using System.Net.Http.Headers;

namespace DownloadAPI.Downloaders
{
    internal sealed class HttpClientDownloader : FileDownloader, IDisposable
    {
        private readonly IRecoderResponceHandler _recoderResponceHandler;
        private readonly HttpClient _client;

        private HttpContentHeaders? _urlHeadersContent;
        private string? _mime;

        public HttpClientDownloader(FileData fileToDownload, FileStorage storage, SupportedTypes type, IRecoderResponceHandler recoderResponceHandler, HttpClient? client = null) 
            : base(fileToDownload, storage, type)
        {
            _client = client ?? new();
            _recoderResponceHandler = recoderResponceHandler;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public override async Task<DownloadResult> TrySetupFileAsync()
        {
            DownloadResult isHeadersSet = await TrySetUrlHeadersContentAsync();

            if (isHeadersSet.IsBadResult())
                return isHeadersSet;

            _mime = _urlHeadersContent.ContentType.MediaType;
            DownloadResult isTypeCorrect = FileToDownload.IsTypeCorrect(_mime, ExpectedMime);

            if (isTypeCorrect.IsBadResult())
                return isTypeCorrect;

            string extension = GetExtension();
            FileToDownload.SetPathWithExtension(extension);

            return await base.TrySetupFileAsync();
        }

        protected override Task<(DownloadResult result, long? sizeInBytes)> TryGetSizeFromApiAsync()
        {
            (DownloadResult, long?) result;

            if (_urlHeadersContent is null)
            {
                result = (DownloadResult.ResponceError, null);
                return Task.FromResult(result);
            }

            long? sizeInBytes = _urlHeadersContent.ContentLength;
            result = (DownloadResult.Ok, sizeInBytes);
            return Task.FromResult(result);
        }

        protected override Task SetFilePathWithExtentionAsync()
        {
            string extension = GetExtension();
            FileToDownload.SetPathWithExtension(extension);
            return Task.CompletedTask;
        }

        protected override async Task<DownloadResult> DownloadAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(FileToDownload.InputedUrl);

            if (response.IsSuccessStatusCode == false)
                return DownloadResult.ResponceError;

            await using var contentStream = await response.Content.ReadAsStreamAsync();
            await using var fileStream = new FileStream(FileToDownload.PathWithExtension, FileMode.Create);
            await contentStream.CopyToAsync(fileStream);

            return DownloadResult.Ok;
        }

        protected override async Task<string> GetCorrectExtensionAsync()
        {
            if (ExpectedMime == FileTypes.ExpectedMime.Video)
                return await _recoderResponceHandler.RecodeVideoIfNeededAsync(FileToDownload.PathWithExtension);

            return DefaultExtension;
        }

        private async Task<DownloadResult> TrySetUrlHeadersContentAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Head, FileToDownload.InputedUrl);
            var response = await _client.SendAsync(request);

            if (IsResponceInvalid(response))
                return DownloadResult.ResponceError;

            _urlHeadersContent = response.Content.Headers;
            return DownloadResult.Ok;
        }

        private string GetExtension()
        {
            string fileExtension = String.IsNullOrWhiteSpace(_mime) == false 
                ? MimeTypesMap.GetExtension(_mime) 
                : DefaultExtension;
            return fileExtension;
        }

        private bool IsResponceInvalid(HttpResponseMessage response)
        {
            return response.IsSuccessStatusCode == false || response.Content is null || response.Content.Headers is null || response.Content.Headers.ContentType is null;
        }
    }
}