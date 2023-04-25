using DownloadAPI;
using DownloadUtilsApi.YtDlp;

namespace UnitTests.DownloadAPI
{
    public class YtDlpResultExtensionsTests
    {
        [Fact]
        public void ToDownloadResult_ShouldReturnOk_WhenYtDlpResultIsOk()
        {
            // Arrange
            YtDlpResult ytDlpResult = YtDlpResult.Ok;

            // Act
            DownloadResult downloadResult = ytDlpResult.ToDownloadResult();

            // Assert
            Assert.Equal(DownloadResult.Ok, downloadResult);
        }

        [Fact]
        public void ToDownloadResult_ShouldReturnFormatNotAvaiable_WhenYtDlpResultIsFormatNotAvailable()
        {
            // Arrange
            YtDlpResult ytDlpResult = YtDlpResult.FormatNotAvaiable;

            // Act
            DownloadResult downloadResult = ytDlpResult.ToDownloadResult();

            // Assert
            Assert.Equal(DownloadResult.FormatNotAvaiable, downloadResult);
        }

        [Fact]
        public void ToDownloadResult_ShouldReturnInvalidInput_WhenYtDlpResultIsOtherError()
        {
            // Arrange
            YtDlpResult ytDlpResult = YtDlpResult.OtherError;

            // Act
            DownloadResult downloadResult = ytDlpResult.ToDownloadResult();

            // Assert
            Assert.Equal(DownloadResult.InvalidInput, downloadResult);
        }
    }
}