using DownloadUtilsApi;
using DownloadUtilsApi.FFmpeg;
using DownloadUtilsApi.Utils;

namespace UnitTests.DownloadUtilsApi.FFmpeg
{
    public class FFmpegOptionsTests
    {
        [Fact]
        public void GetInput_ShouldReturnCorrectInputOption()
        {
            // Arrange
            string testInputPath = "testInputPath";
            string expectedOption = OptionsUtils.GetOptionAsCommand(OptionsValues.FFmpeg.Commands.Input, testInputPath);

            // Act
            string result = Options.GetInput(testInputPath);

            // Assert
            Assert.Equal(expectedOption, result);
        }

        [Fact]
        public void GetVideoCodec_ShouldReturnCorrectVideoCodecOption()
        {
            // Arrange
            string expectedOption = OptionsUtils.GetOptionAsCommand(OptionsValues.FFmpeg.Commands.VideoCodec, OptionsValues.FFmpeg.Values.H264Codec);

            // Act
            string result = Options.GetVideoCodec();

            // Assert
            Assert.Equal(expectedOption, result);
        }
    }
}