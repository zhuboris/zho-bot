using DownloadAPI.Files;
using GlobalUtils;

namespace UnitTests.DownloadAPI.Files
{
    public class SupportedTypesExtensionTests
    {
        [Fact]
        public void ExpectedMime_ShouldReturnCorrectMimeForVideo()
        {
            // Arrange
            SupportedTypes type = SupportedTypes.Video;

            // Act
            string expectedMime = type.ExpectedMime();

            // Assert
            Assert.Equal(FileTypes.ExpectedMime.Video, expectedMime);
        }

        [Fact]
        public void ExpectedMime_ShouldReturnCorrectMimeForGif()
        {
            // Arrange
            SupportedTypes type = SupportedTypes.Gif;

            // Act
            string expectedMime = type.ExpectedMime();

            // Assert
            Assert.Equal(FileTypes.ExpectedMime.Gif, expectedMime);
        }

        [Fact]
        public void DefaultExtension_ShouldReturnCorrectExtensionForVideo()
        {
            // Arrange
            SupportedTypes type = SupportedTypes.Video;

            // Act
            string defaultExtension = type.DefaultExtension();

            // Assert
            Assert.Equal(FileTypes.DefaultExtentions.Video, defaultExtension);
        }

        [Fact]
        public void DefaultExtension_ShouldReturnCorrectExtensionForGif()
        {
            // Arrange
            SupportedTypes type = SupportedTypes.Gif;

            // Act
            string defaultExtension = type.DefaultExtension();

            // Assert
            Assert.Equal(FileTypes.DefaultExtentions.Gif, defaultExtension);
        }
    }
}