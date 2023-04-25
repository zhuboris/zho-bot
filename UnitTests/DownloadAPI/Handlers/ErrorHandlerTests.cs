using DownloadAPI;
using DownloadAPI.Handlers;
using GlobalUtils;

namespace UnitTests.DownloadAPI.Handlers
{
    public class ErrorHandlerTests
    {
        [Fact]
        public void GetErrorMessage_ReturnsEmptyString_WhenResultIsOk()
        {
            // Act
            string errorMessage = ErrorHandler.GetErrorMessage(DownloadResult.Ok);

            // Assert
            Assert.Empty(errorMessage);
        }

        [Theory]
        [InlineData(8)]
        [InlineData(0)]
        [InlineData(100)]
        public void GetErrorMessage_ReturnsCorrectFormattedMessage_WhenResultIsSizeLimitExceeded(int sizeInMb)
        {
            const long BytesInOneMb = 1048576L;

            //Arrange
            long sizeInBytes = sizeInMb * BytesInOneMb;

            // Act
            string errorMessage = ErrorHandler.GetErrorMessage(DownloadResult.SizeLimitExceeded, sizeInBytes);

            // Assert
            Assert.Equal($"File is too big. Maximum size is {sizeInMb} mb.", errorMessage);
        }

        [Theory]
        [InlineData((int)DownloadResult.InvalidInput, MessagesText.Errors.WrongInput)]
        [InlineData((int)DownloadResult.ResponceError, MessagesText.Errors.ConnectionError)]
        [InlineData((int)DownloadResult.IncorrectFileType, MessagesText.Errors.WrongType)]
        [InlineData((int)DownloadResult.ResultIsNull, MessagesText.Errors.DownloadError)]
        [InlineData((int)DownloadResult.PathNotExist, MessagesText.Errors.DownloadError)]
        [InlineData((int)DownloadResult.QueueIsFull, MessagesText.Errors.QueueIsFull)]
        public void GetErrorMessage_ReturnsCorrectMessage_WhenResultIsOtherBadResult(int resultValue, string expectedMessage)
        {
            //Arrange
            var result = (DownloadResult)resultValue;

            // Act
            string errorMessage = ErrorHandler.GetErrorMessage(result);

            // Assert
            Assert.Equal(expectedMessage, errorMessage);
        }
    }
}