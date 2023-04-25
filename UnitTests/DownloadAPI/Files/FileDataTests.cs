using DownloadAPI;
using DownloadAPI.Files;
using GlobalUtils;

namespace UnitTests.DownloadAPI.Files
{
    public class FileDataTests
    {
        public const string TxtExtension = ".txt";
        public const string ExistingFilePath = "testPath";

        private readonly FileData _sut;

        public FileDataTests()
        {
            _sut = new FileData("https://example.com", ExistingFilePath, 1000);
            _sut.SetPathWithExtension(TxtExtension);
        }

        [Theory]
        [InlineData("video/mp4", FileTypes.ExpectedMime.Video)]
        [InlineData("video/", FileTypes.ExpectedMime.Video)]
        [InlineData("image/gif", FileTypes.ExpectedMime.Gif)]
        public void IsTypeCorrect_ShouldReturnOk_WhenTypeStartsWithExpectedValue(string type, string expectedMime)
        {
            // Act
            DownloadResult result = _sut.IsTypeCorrect(type, expectedMime);

            // Assert
            Assert.Equal(DownloadResult.Ok, result);
        }

        [Theory]
        [InlineData("audio/mp3", FileTypes.ExpectedMime.Video)]
        [InlineData("video/", FileTypes.ExpectedMime.Gif)]
        [InlineData("image/gif", FileTypes.ExpectedMime.Video)]
        public void IsTypeCorrect_ShouldReturnIncorrectFileType_WhenTypeDoesNotStartWithExpectedValue(string type, string expectedMime)
        {
            // Act
            DownloadResult result = _sut.IsTypeCorrect(type, expectedMime);

            // Assert
            Assert.Equal(DownloadResult.IncorrectFileType, result);
        }

        [Theory]
        [InlineData(null, FileTypes.ExpectedMime.Video)]
        [InlineData("", FileTypes.ExpectedMime.Gif)]
        [InlineData("   ", FileTypes.ExpectedMime.Gif)]
        public void IsTypeCorrect_ShouldReturnInvalidInput_WhenTypeIsNukkOrWhiteSpace(string? type, string expectedMime)
        {
            // Act
            DownloadResult result = _sut.IsTypeCorrect(type, expectedMime);

            // Assert
            Assert.Equal(DownloadResult.InvalidInput, result);
        }

        [Fact]
        public void SetPathWithExtension_ShouldSetPathWithExtension()
        {
            // Arrange            
            string extension = VideoParameters.Extensions.Mp4;

            // Act
            _sut.SetPathWithExtension(extension);

            // Assert
            Assert.Equal("testPath.mp4", _sut.PathWithExtension);
        }

        [Fact]
        public void TryGetCurrentSize_ShouldReturnSizeAndTrue_WhenFileExists()
        {
            // Arrange
            string pathToFile = Path.ChangeExtension(ExistingFilePath, TxtExtension);
            File.WriteAllText(pathToFile, "Test content");

            // Act
            bool result = _sut.TryGetCurrentSize(out long size);

            // Assert
            Assert.True(result);
            Assert.Equal(new FileInfo(pathToFile).Length, size);

            // Cleanup
            File.Delete(pathToFile);
        }

        [Fact]
        public void TryGetCurrentSize_ShouldReturnFalse_WhenFileDoesNotExist()
        {
            // Arrange
            var fileData = new FileData("https://example.com", "nonexistent", 1000);

            // Act
            bool result = fileData.TryGetCurrentSize(out long _);

            // Assert
            Assert.False(result);
        }
    }
}