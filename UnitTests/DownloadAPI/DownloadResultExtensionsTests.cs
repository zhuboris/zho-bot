using DownloadAPI;
using UnitTests.TestSetups;

namespace UnitTests.DownloadAPI
{
    public class DownloadResultExtensionsTests
    {
        public static readonly IReadOnlyCollection<object[]> AllBadResults = TestGlobalConstants.AllBadResultsValues;

        [Theory]
        [MemberData(nameof(AllBadResults))]
        public void IsBadResult_ShouldReturnTrue_WhenResultIsNotOk(int badResultValue)
        {
            // Arrange
            DownloadResult badResult = (DownloadResult)badResultValue;

            // Act
            bool isBadResult = badResult.IsBadResult();

            // Assert
            Assert.True(isBadResult);
        }

        [Fact]
        public void IsBadResult_ShouldReturnFalse_WhenResultIsOk()
        {
            // Arrange
            DownloadResult result = DownloadResult.Ok;

            // Act
            bool isBadResult = result.IsBadResult();

            // Assert
            Assert.False(isBadResult);
        }

        [Fact]
        public void ConvertToNotNullable_ShouldReturnResult_WhenNotNull()
        {
            // Arrange
            DownloadResult? result = DownloadResult.Ok;

            // Act
            DownloadResult convertedResult = result.ConvertToNotNullable();

            // Assert
            Assert.Equal(DownloadResult.Ok, convertedResult);
        }

        [Fact]
        public void ConvertToNotNullable_ShouldReturnResultIsNull_WhenNull()
        {
            // Arrange
            DownloadResult? result = null;

            // Act
            DownloadResult convertedResult = result.ConvertToNotNullable();

            // Assert
            Assert.Equal(DownloadResult.ResultIsNull, convertedResult);
        }
    }
}