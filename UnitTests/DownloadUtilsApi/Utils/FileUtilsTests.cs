using DownloadUtilsApi.Utils;
using GlobalUtils;

namespace UnitTests.DownloadUtilsApi.Utils
{
    public class FileUtilsTests : IDisposable
    {
        private const string TestFileName = "test.txt";
        private const string TestFileNameWithoutExtension = "test";
        private const string TempPostfix = GlobalConstants.TempPostfix;
        private const string TestFileContent = "Test content";

        private readonly string _testFilePath;
        private readonly string _currentDirectory;

        public FileUtilsTests()
        {
            _currentDirectory = Directory.GetCurrentDirectory();
            _testFilePath = CreateTestFile();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
        }

        [Fact]
        public void GetFullPathWithoutExtention_ShouldReturnPathWithoutExtension()
        {
            // Arrange
            string expectedPathWithoutExtension = Path.Combine(_currentDirectory, TestFileNameWithoutExtension);

            // Act
            string actualPathWithoutExtension = FileUtils.GetFullPathWithoutExtention(_testFilePath);

            // Assert
            Assert.Equal(expectedPathWithoutExtension, actualPathWithoutExtension);
        }

        [Fact]
        public void RenameFileWithPostfix_ShouldRenameFileAndReturnNewPath()
        {
            // Arrange
            string expectedNewPath = Path.Combine(_currentDirectory, $"{TestFileNameWithoutExtension}{TempPostfix}.txt");

            // Act
            string actualNewPath = FileUtils.RenameFileWithPostfix(_testFilePath);

            // Assert
            Assert.Equal(expectedNewPath, actualNewPath);
            Assert.True(File.Exists(actualNewPath));
            FileUtils.Delete(actualNewPath);
        }

        [Fact]
        public void RenameFileBack_ShouldRenameFileBackAndReturnOriginalPath()
        {
            // Arrange
            string pathWithPostfix = FileUtils.RenameFileWithPostfix(_testFilePath);

            // Act
            string actualOriginalPath = FileUtils.RenameFileBack(pathWithPostfix);

            // Assert
            Assert.Equal(_testFilePath, actualOriginalPath);
            Assert.True(File.Exists(actualOriginalPath));
        }

        private string CreateTestFile()
        {
            string testFilePath = Path.Combine(_currentDirectory, TestFileName);
            File.WriteAllText(testFilePath, TestFileContent);
            return testFilePath;
        }
    }
}
