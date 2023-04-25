using DownloadUtilsApi.YtDlp;
using DownloadUtilsApi.YtDlp.Handlers;

namespace UnitTests.DownloadUtilsApi.Utils
{
    public class ErrorHandlerTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("Some text")]
        public void GetResult_ShouldReturnOk_WhenNoError(string errorOutput)
        {
            // Act
            YtDlpResult result = ErrorHandler.GetResult(errorOutput);

            // Assert
            Assert.Equal(YtDlpResult.Ok, result);
        }

        [Theory]
        [InlineData("ERROR: requested format is not available")]
        [InlineData("Some text before the error: ERROR: requested format is not available")]
        [InlineData("Some text before the error: \nERROR: requested format is not available")]
        [InlineData("ERROR: Some other error occurred \nERROR: requested format is not available")]
        public void GetResult_ShouldReturnFormatNotAvailable_WhenFormatNotAvailableError(string errorOutput)
        {
            // Act
            YtDlpResult result = ErrorHandler.GetResult(errorOutput);

            // Assert
            Assert.Equal(YtDlpResult.FormatNotAvaiable, result);
        }

        [Theory]
        [InlineData("ERROR: Some other error occurred")]
        [InlineData("Some text before the error: ERROR: Some other error occurred")]
        [InlineData("Some text before the error: \nERROR: Some other error occurred")]
        public void GetResult_ShouldReturnOtherError_WhenOtherError(string errorOutput)
        {
            // Act
            YtDlpResult result = ErrorHandler.GetResult(errorOutput);

            // Assert
            Assert.Equal(YtDlpResult.OtherError, result);
        }
    }
}
