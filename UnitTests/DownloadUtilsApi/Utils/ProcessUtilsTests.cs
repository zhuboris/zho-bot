using DownloadUtilsApi.Utils;

namespace UnitTests.DownloadUtilsApi.Utils
{
    public class ProcessUtilsTests
    {
        private const string RelativeAppPath = "Echo\\Echo.exe";
        private const string TestCommand = "Hello, World!";

        private readonly string _appForTestPath;

        public ProcessUtilsTests()
        {
            string pathToRoot = GetPathToProjectRoot();
            _appForTestPath = Path.Combine(pathToRoot, RelativeAppPath);
        }

        [Fact]
        public async Task ExecuteProcessWithOutputAsync_ShouldReturnOutputAndEmptyErrors_WhenCorrectCommand()
        {
            // Act
            var (errors, output) = await ProcessUtils.ExecuteProcessWithOutputAsync(_appForTestPath, TestCommand);

            // Assert
            Assert.Empty(errors);
            Assert.Equal(TestCommand, output.Trim());
        }

        [Fact]
        public async Task ExecuteProcessAsync_ShouldReturnEmptyErrors_WhenCorrectCommand()
        {
            // Act
            string errors = await ProcessUtils.ExecuteProcessAsync(_appForTestPath, TestCommand);

            // Assert
            Assert.Empty(errors);
        }

        [Fact]
        public async Task ExecuteProcessWithOutputAsync_ShouldReturnErrorsAndEmptyOutput_WhenIncorrectCommand()
        {
            // Arrange
            string incorrectCommand = String.Empty;

            // Act
            var (errors, output) = await ProcessUtils.ExecuteProcessWithOutputAsync(_appForTestPath, incorrectCommand);

            // Assert
            Assert.NotEmpty(errors);
            Assert.NotNull(output);
        }

        [Fact]
        public async Task ExecuteProcessAsync_ShouldReturnErrors_WhenIncorrectCommand()
        {
            // Arrange
            string incorrectCommand = String.Empty;

            // Act
            string errors = await ProcessUtils.ExecuteProcessAsync(_appForTestPath, incorrectCommand);

            // Assert
            Assert.NotEmpty(errors);
        }

        [Fact]
        public async Task ExecuteProcessWithOutputAsync_ShouldReturnErrors_WhenInvalidAppPath()
        {
            // Arrange
            string invalidAppPath = "invalid_app_path";

            // Act
            var (errors, output) = await ProcessUtils.ExecuteProcessWithOutputAsync(invalidAppPath, TestCommand);

            // Assert
            Assert.NotEmpty(errors);
            Assert.NotNull(output);
        }

        [Fact]
        public async Task ExecuteProcessAsync_ShouldReturnErrors_WhenInvalidAppPath()
        {
            // Arrange
            string invalidAppPath = "invalid_app_path";

            // Act
            string errors = await ProcessUtils.ExecuteProcessAsync(invalidAppPath, TestCommand);

            // Assert
            Assert.NotEmpty(errors);
        }

        private static string GetPathToProjectRoot()
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo? rootDirectory = Directory.GetParent(projectPath)?.Parent?.Parent?.Parent;
            string pathToRoot = rootDirectory?.FullName ?? projectPath;
            return pathToRoot;
        }
    }
}