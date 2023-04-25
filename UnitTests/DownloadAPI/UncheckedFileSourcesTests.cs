using DownloadAPI;

namespace UnitTests.DownloadAPI
{
    public class UncheckedFileSourcesTests
    {
        [Theory]
        [InlineData("https://t.me/somechannel")]
        [InlineData("http://t.me/somechannel")]
        public void IsInUnchecked_ShouldReturnTrue_WhenUrlIsInUncheckedSources(string url)
        {
            // Act
            bool isInUnchecked = UncheckedFileSources.IsInUnckecked(url);

            // Assert
            Assert.True(isInUnchecked);
        }

        [Theory]
        [InlineData("https://youtube.com/somechannel")]
        [InlineData("http://example.org/somechannel")]
        [InlineData("invalidUrl")]
        public void IsInUnchecked_ShouldReturnFalse_WhenUrlIsNotInUncheckedSources(string url)
        {
            // Act
            bool isInUnchecked = UncheckedFileSources.IsInUnckecked(url);

            // Assert
            Assert.False(isInUnchecked);
        }
    }
}