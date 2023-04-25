using DownloadUtilsApi;
using DownloadUtilsApi.Utils;
using DownloadUtilsApi.YtDlp;

namespace UnitTests.DownloadUtilsApi.YtDlp
{
    public class OptionsTests
    {
        [Theory]
        [InlineData("path/to/file", @"path\to\file.%(ext)s")]
        [InlineData("path/to/file.txt", @"path\to\file.%(ext)s")]
        public void GetPath_ShouldReturnCorrectPath(string path, string expectedPath)
        {
            // Arrange
            string expectedCommand = OptionsUtils.GetOptionAsCommand(OptionsValues.YtDlp.Commands.ChangePath, expectedPath);

            // Act
            string actualCommand = Options.GetPath(path);

            // Assert
            Assert.Equal(expectedCommand, actualCommand);
        }

        [Fact]
        public void GetMaxNumberOfVideos_ShouldReturnCorrectMaxNumberOfVideosCommand()
        {
            // Arrange
            string expectedCommand = $"{OptionsValues.YtDlp.Commands.SetMaxNumberOfVideos} {OptionsValues.YtDlp.Values.MaxNumberOfVideos}";

            // Act
            string actualCommand = Options.GetMaxNumberOfVideos();

            // Assert
            Assert.Equal(expectedCommand, actualCommand);
        }

        [Fact]
        public void GetFormat_ShouldReturnCorrectFormatCommand()
        {
            // Arrange
            long maxSize = 10485760L;
            string expectedFormat = OptionsValues.YtDlp.Values.GetFormatOptionValue(maxSize);
            string expectedCommand = $"{OptionsValues.YtDlp.Commands.SetFormat} {expectedFormat}";

            // Act
            string actualCommand = Options.GetFormat(maxSize);

            // Assert
            Assert.Equal(expectedCommand, actualCommand);
        }

        [Fact]
        public void GetSize_ShouldReturnCorrectSizeCommand()
        {
            // Arrange
            string expectedCommand = $"{OptionsValues.YtDlp.Commands.GetInfo} {OptionsValues.YtDlp.Values.SizeOption}";

            // Act
            string actualCommand = Options.GetSize();

            // Assert
            Assert.Equal(expectedCommand, actualCommand);
        }

        [Fact]
        public void GetFileName_ShouldReturnCorrectFileNameCommand()
        {
            // Arrange
            string expectedCommand = $"{OptionsValues.YtDlp.Commands.GetInfo} {OptionsValues.YtDlp.Values.FileNameOption}";

            // Act
            string actualCommand = Options.GetFileName();

            // Assert
            Assert.Equal(expectedCommand, actualCommand);
        }
    }
}