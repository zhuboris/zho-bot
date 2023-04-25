using DownloadAPI;
using DownloadAPI.Downloaders;
using DownloadAPI.Files;
using DownloadUtilsApi.DependencyInjection.ResponceHandlers;
using GlobalUtils;
using Moq;
using System.Net;
using System.Net.Http.Headers;
using UnitTests.TestSetups;

namespace UnitTests.DownloadAPI.Downloaders
{
    public class HttpClientDownloaderTests
    {
        private const string TestUrl = "https://example.com/video.mp4";
        private const long MaxSize = 52428800L;
        private const long SuitableSize = 1L;
        private const long TooBigSize = 52428801L;

        private readonly IRecoderResponceHandler _recorder;
        private readonly FileData _file;
        private readonly FileStorage _storage;
        private readonly SupportedTypes _type;

        public HttpClientDownloaderTests()
        {
            var recorderMock = new Mock<IRecoderResponceHandler>();
            recorderMock.SetupRecodeVideoIfNeededAsync();
            _recorder = recorderMock.Object;

            _file = new FileData(TestUrl, "testFileForHttpTest", MaxSize);
            _storage = new FileStorage("testFolder");
            _type = SupportedTypes.Video;
        }

        [Fact]
        public async Task TrySetupFileAsync_ShouldReturnResponceError_WhenMessageContainsBadCode()
        {
            // Arrange
            HttpMessageHandler httpMessageHandler = GetMessageWithBadCode();
            var httpClient = new HttpClient(httpMessageHandler);

            var sut = new HttpClientDownloader(_file, _storage, _type, _recorder, httpClient);

            // Act
            var result = await sut.TrySetupFileAsync();

            // Assert
            Assert.Equal(DownloadResult.ResponceError, result);
        }

        [Fact]
        public async Task TrySetupFileAsync_ShouldReturnResponceError_WhenMessageNotContainsContext()
        {
            // Arrange
            var httpMessageHandler = new Mock<HttpMessageHandler>();
            httpMessageHandler.SetupResponceWithoutContext();
            var httpClient = new HttpClient(httpMessageHandler.Object);

            var sut = new HttpClientDownloader(_file, _storage, _type, _recorder, httpClient);

            // Act
            var result = await sut.TrySetupFileAsync();

            // Assert
            Assert.Equal(DownloadResult.ResponceError, result);
        }

        [Fact]
        public async Task TrySetupFileAsync_ShouldReturnResponceError_WhenMessageContainsContextWithoutMime()
        {
            // Arrange
            HttpMessageHandler httpMessageHandler = GetMessageWithoutMime();
            var httpClient = new HttpClient(httpMessageHandler);

            var sut = new HttpClientDownloader(_file, _storage, _type, _recorder, httpClient);

            // Act
            var result = await sut.TrySetupFileAsync();

            // Assert
            Assert.Equal(DownloadResult.ResponceError, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(0L)]
        [InlineData(-1L)]
        public async Task TrySetupFileAsync_ShouldReturnResponceError_WhenMessageContainsInvalidSize(long? invalidLength)
        {
            // Arrange
            HttpMessageHandler httpMessageHandler = GetMessageWithWrongSize(invalidLength);
            var httpClient = new HttpClient(httpMessageHandler);

            var sut = new HttpClientDownloader(_file, _storage, _type, _recorder, httpClient);

            // Act
            var result = await sut.TrySetupFileAsync();

            // Assert
            Assert.Equal(DownloadResult.ResponceError, result);
        }

        [Fact]
        public async Task TrySetupFileAsync_ShouldReturnSizeLimitExceeded_WhenMessageContainsTooBigSize()
        {
            // Arrange
            HttpMessageHandler httpMessageHandler = GetMessageWithWrongSize(TooBigSize);
            var httpClient = new HttpClient(httpMessageHandler);

            var sut = new HttpClientDownloader(_file, _storage, _type, _recorder, httpClient);

            // Act
            var result = await sut.TrySetupFileAsync();

            // Assert
            Assert.Equal(DownloadResult.SizeLimitExceeded, result);
        }

        [Theory]
        [InlineData("audio/mp3")]
        [InlineData("image/gif")]
        public async Task TrySetupFileAsync_ShouldReturnIncorrectFileType_WhenMessageContainsIncorrectMime(string incorrectMime)
        {
            // Arrange
            HttpMessageHandler httpMessageHandler = GetMessageWithMime(incorrectMime);
            var httpClient = new HttpClient(httpMessageHandler);

            var sut = new HttpClientDownloader(_file, _storage, _type, _recorder, httpClient);

            // Act
            var result = await sut.TrySetupFileAsync();

            // Assert
            Assert.Equal(DownloadResult.IncorrectFileType, result);
        }

        [Fact]
        public async Task TrySetupFileAsync_ShouldReturnOk_WhenMessageContainsTooBigSizeButSizeCheckSkipped()
        {
            // Arrange
            HttpMessageHandler httpMessageHandler = GetMessageWithWrongSize(TooBigSize);
            var httpClient = new HttpClient(httpMessageHandler);
            var file = new FileData(TestUrl, "testFileWithSkip", MaxSize, true);

            var sut = new HttpClientDownloader(file, _storage, _type, _recorder, httpClient);

            // Act
            var result = await sut.TrySetupFileAsync();

            // Assert
            Assert.Equal(DownloadResult.Ok, result);
            Assert.Contains(Path.GetExtension(file.PathWithExtension), TestGlobalConstants.ExpectedVideoExtensions);
            Assert.True(File.Exists(file.PathWithExtension));

            //Cleanup
            Cleanup.DeleteFileIfItExists(file.PathWithExtension);
        }

        [Fact]
        public async Task TrySetupFileAsync_ShouldReturnOk_WhenMessageContainsCorrectMimeAndSize_ForGif()
        {
            // Arrange
            HttpMessageHandler httpMessageHandler = GetMessageWithMime(FileTypes.ExpectedMime.Gif);
            var httpClient = new HttpClient(httpMessageHandler);

            var sut = new HttpClientDownloader(_file, _storage, SupportedTypes.Gif, _recorder, httpClient);

            // Act
            var result = await sut.TrySetupFileAsync();

            // Assert
            Assert.Equal(DownloadResult.Ok, result);
            Assert.True(Path.GetExtension(_file.PathWithExtension) == FileTypes.DefaultExtentions.Gif);
            Assert.True(File.Exists(_file.PathWithExtension));

            //Cleanup
            Cleanup.DeleteFileIfItExists(_file.PathWithExtension);
        }

        [Theory]
        [InlineData("video/mp4")]
        [InlineData("video/webm")]
        [InlineData("video/x-flv")]
        [InlineData("video/3gpp2")]
        public async Task TrySetupFileAsync_ShouldReturnOk_WhenMessageContainsCorrectMimeAndSize_ForVideo(string correctMime)
        {
            // Arrange
            HttpMessageHandler httpMessageHandler = GetMessageWithMime(correctMime);
            var httpClient = new HttpClient(httpMessageHandler);

            var sut = new HttpClientDownloader(_file, _storage, _type, _recorder, httpClient);

            // Act
            var result = await sut.TrySetupFileAsync();

            // Assert
            Assert.Equal(DownloadResult.Ok, result);
            Assert.Contains(Path.GetExtension(_file.PathWithExtension), TestGlobalConstants.ExpectedVideoExtensions);
            Assert.True(File.Exists(_file.PathWithExtension));

            //Cleanup
            Cleanup.DeleteFileIfItExists(_file.PathWithExtension);
        }

        private static HttpMessageHandler GetMessageWithBadCode()
        {
            var mediaTypeHeaderValue = new MediaTypeHeaderValue("video/mp4");

            var httpMessageHandler = new Mock<HttpMessageHandler>();
            httpMessageHandler.SetupResponce(HttpStatusCode.BadRequest, mediaTypeHeaderValue, SuitableSize);
            return httpMessageHandler.Object;
        }

        private static HttpMessageHandler GetMessageWithoutMime()
        {
            MediaTypeHeaderValue? mediaTypeHeaderValue = null;

            var httpMessageHandler = new Mock<HttpMessageHandler>();
            httpMessageHandler.SetupResponce(HttpStatusCode.BadRequest, mediaTypeHeaderValue, SuitableSize);
            return httpMessageHandler.Object;
        }

        private static HttpMessageHandler GetMessageWithWrongSize(long? wrongLength)
        {
            var mediaTypeHeaderValue = new MediaTypeHeaderValue("video/mp4");

            var httpMessageHandler = new Mock<HttpMessageHandler>();
            httpMessageHandler.SetupResponce(HttpStatusCode.OK, mediaTypeHeaderValue, wrongLength);
            return httpMessageHandler.Object;
        }

        private static HttpMessageHandler GetMessageWithMime(string incorrectMime)
        {
            var mediaTypeHeaderValue = new MediaTypeHeaderValue(incorrectMime);

            var httpMessageHandler = new Mock<HttpMessageHandler>();
            httpMessageHandler.SetupResponce(HttpStatusCode.OK, mediaTypeHeaderValue, SuitableSize);
            return httpMessageHandler.Object;
        }
    }
}