using DownloadUtilsApi.DependencyInjection.ProcessExecuters;
using DownloadUtilsApi.YtDlp;
using DownloadUtilsApi.YtDlp.Handlers;
using Moq;
using UnitTests.TestSetups;

namespace UnitTests.DownloadUtilsApi.Utils
{
    public class YtDlpResponceHandlerTests
    {
        private const long MinSize = 1L;
        private const long MaxSize = 10485760L;

        private readonly YtDlpResponceHandler _sut;

        public YtDlpResponceHandlerTests()
        {
            var mock = new Mock<IDownloaderProcessExecuter>();
            mock.SetupAllMethods();

            _sut = new YtDlpResponceHandler(mock.Object);
        }                

        [Fact]
        public async Task TryGetSizeAsync_ShouldReturnOtherErrorAndNull_WhenInvalidUrl()
        {
            // Act
            var (result, size) = await _sut.TryGetSizeAsync(TestGlobalConstants.InvalidUrl, TestGlobalConstants.TestPath, MaxSize);

            // Assert
            Assert.Equal(YtDlpResult.OtherError, result);
            Assert.Null(size);
        }

        [Fact]
        public async Task TryGetSizeAsync_ShouldReturnFormatNotAvaiableAndNull_WhenValidUrlAndSizeTooMuch()
        {
            // Act
            var (result, size) = await _sut.TryGetSizeAsync(TestGlobalConstants.UrlOfOversizedVideo, TestGlobalConstants.TestPath, MaxSize);

            // Assert
            Assert.Equal(YtDlpResult.FormatNotAvaiable, result);
            Assert.Null(size);
        }

        [Fact]
        public async Task TryGetSizeAsync_ShouldReturnOkAndCorrectSize_WhenValidUrlAndSizeIsFit()
        {
            // Act
            var (result, size) = await _sut.TryGetSizeAsync(TestGlobalConstants.UrlOfSuitableSizeVideo, TestGlobalConstants.TestPath, MaxSize);

            // Assert
            Assert.Equal(YtDlpResult.Ok, result);
            Assert.NotNull(size);
            Assert.InRange((long)size, MinSize, MaxSize);
        }

        [Fact]
        public async Task GetExtensionAsync_ShouldReturnNull_WhenInvalidUrl()
        {
            // Act
            string? extension = await _sut.GetExtensionAsync(TestGlobalConstants.InvalidUrl, TestGlobalConstants.TestPath, MaxSize);

            // Assert
            Assert.Null(extension);
        }

        [Fact]
        public async Task GetExtensionAsync_ShouldReturnNull_WhenValidUrlAndSizeTooMuch()
        {
            // Act
            string? extension = await _sut.GetExtensionAsync(TestGlobalConstants.InvalidUrl, TestGlobalConstants.TestPath, MaxSize);

            // Assert
            Assert.Null(extension);
        }

        [Fact]
        public async Task GetExtensionAsync_ShouldReturnExtension_WhenValidUrlAndSizeFit()
        {
            // Act
            string? extension = await _sut.GetExtensionAsync(TestGlobalConstants.UrlOfSuitableSizeVideo, TestGlobalConstants.TestPath, MaxSize);

            // Assert
            Assert.NotNull(extension);
            Assert.Equal(TestGlobalConstants.TestExtension, extension);
        }

        [Fact]
        public async Task TryDownloadAsync_ShouldReturnOtherError_WhenInvalidUrl()
        {
            // Arrange
            string pathWithExt = $"1{TestGlobalConstants.TestPath}.mp4";

            // Act
            YtDlpResult result = await _sut.TryDownloadAsync(TestGlobalConstants.InvalidUrl, pathWithExt, MaxSize);

            // Assert
            Assert.Equal(YtDlpResult.OtherError, result);
            Assert.False(File.Exists(pathWithExt));

            // Cleanup
            DeleteDownloadedFile(pathWithExt);
        }

        [Fact]
        public async Task TryDownloadAsync_ShouldReturnFormatNotAvaiable_WhenValidUrlAndSizeTooMuch()
        {
            // Arrange
            string pathWithExt = $"2{TestGlobalConstants.TestPath}.mp4";

            // Act
            YtDlpResult result = await _sut.TryDownloadAsync(TestGlobalConstants.UrlOfOversizedVideo, pathWithExt, MaxSize);

            // Assert
            Assert.Equal(YtDlpResult.FormatNotAvaiable, result);
            Assert.False(File.Exists(pathWithExt));

            // Cleanup
            DeleteDownloadedFile(pathWithExt);
        }

        [Fact]
        public async Task TryDownloadAsync_ShouldReturnOkAndDownloadFile_WhenValidUrlAndSizeIsFit()
        {
            // Arrange
            string pathWithExt = $"3{TestGlobalConstants.TestPath}.mp4";

            // Act
            YtDlpResult result = await _sut.TryDownloadAsync(TestGlobalConstants.UrlOfSuitableSizeVideo, pathWithExt, MaxSize);

            // Assert
            Assert.Equal(YtDlpResult.Ok, result);
            Assert.True(File.Exists(pathWithExt));

            // Cleanup
            DeleteDownloadedFile(pathWithExt);
        }

        [Fact]
        public async Task TryDownloadWithoutSizeCheckAsync_ShouldReturnOtherError_WhenInvalidUrl()
        {
            // Arrange
            string pathWithExt = $"4{TestGlobalConstants.TestPath}.mp4";

            // Act
            YtDlpResult result = await _sut.TryDownloadWithoutSizeCheckAsync(TestGlobalConstants.InvalidUrl, pathWithExt);

            // Assert
            Assert.Equal(YtDlpResult.OtherError, result);
            Assert.False(File.Exists(pathWithExt));

            // Cleanup
            DeleteDownloadedFile(pathWithExt);
        }

        [Fact]
        public async Task TryDownloadWithoutSizeCheckAsync_ShouldReturnOk_WhenValidUrl()
        {
            // Arrange
            string pathWithExt = $"5{TestGlobalConstants.TestPath}.mp4";

            // Act
            YtDlpResult result = await _sut.TryDownloadWithoutSizeCheckAsync(TestGlobalConstants.UrlOfSuitableSizeVideo, pathWithExt);

            // Assert
            Assert.Equal(YtDlpResult.Ok, result);
            Assert.True(File.Exists(pathWithExt));

            // Cleanup
            DeleteDownloadedFile(pathWithExt);
        }

        private static void DeleteDownloadedFile(string pathWithExt)
        {
            if (File.Exists(pathWithExt))
                File.Delete(pathWithExt);
        }
    }
}