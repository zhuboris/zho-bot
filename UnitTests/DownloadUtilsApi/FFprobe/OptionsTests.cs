using DownloadUtilsApi;
using DownloadUtilsApi.FFprobe;
using DownloadUtilsApi.Utils;

namespace UnitTests.DownloadUtilsApi.FFprobe
{
    public class FFprobeOptionsTests
    {
        [Fact]
        public void GetErrorLevelSetiings_ShouldReturnErrorLevelSettingsOption()
        {
            // Arrange
            string expectedCommand = OptionsUtils.GetOptionAsCommand(OptionsValues.FFprobe.Commands.LoggingLevelSettings, OptionsValues.FFprobe.Values.ErrorLevel);

            // Act
            string actualCommand = Options.GetErrorLevelSetiings();

            // Assert
            Assert.Equal(expectedCommand, actualCommand);
        }

        [Fact]
        public void GetStream_ShouldReturnSelectStreamsOption()
        {
            // Arrange
            string expectedCommand = OptionsUtils.GetOptionAsCommand(OptionsValues.FFprobe.Commands.SelectStreams, OptionsValues.FFprobe.Values.FirstVideoStream);

            // Act
            string actualCommand = Options.GetStream();

            // Assert
            Assert.Equal(expectedCommand, actualCommand);
        }

        [Fact]
        public void GetOutputParams_ShouldReturnOutputParamsOption()
        {
            // Arrange
            string expectedCommand = OptionsUtils.GetOptionAsCommand(OptionsValues.FFprobe.Commands.SetOutputParams, OptionsValues.FFprobe.Values.OutputParams);

            // Act
            string actualCommand = Options.GetOutputParams();

            // Assert
            Assert.Equal(expectedCommand, actualCommand);
        }

        [Fact]
        public void GetOutputFormat_ShouldReturnOutputFormatOption()
        {
            // Arrange
            string expectedCommand = OptionsUtils.GetOptionAsCommand(OptionsValues.FFprobe.Commands.SetOutputFormat, OptionsValues.FFprobe.Values.OutputFormat);

            // Act
            string actualCommand = Options.GetOutputFormat();

            // Assert
            Assert.Equal(expectedCommand, actualCommand);
        }
    }
}
