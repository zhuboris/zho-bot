using DownloadUtilsApi.Utils;

namespace UnitTests.DownloadUtilsApi.Utils
{
    public class OptionsUtilsTests
    {
        [Fact]
        public void GetOptionAsCommand_ShouldReturnCommandWithOptionValue()
        {
            // Arrange
            string optionCommand = "-o";
            string optionValue = "output.mp4";
            string expectedCommand = $"{optionCommand} {optionValue}";

            // Act
            string actualCommand = OptionsUtils.GetOptionAsCommand(optionCommand, optionValue);

            // Assert
            Assert.Equal(expectedCommand, actualCommand);
        }
    }
}