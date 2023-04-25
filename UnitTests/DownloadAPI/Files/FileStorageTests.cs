using DownloadAPI;
using DownloadAPI.Files;
using GlobalUtils;
using UnitTests.TestSetups;

namespace UnitTests.DownloadAPI.Files
{
    public class FileStorageTests
    {
        private const string TestFolderPath = "test-folder";
        private const long CorrectFileSizeInBytes = 1048576L;
        private const long SizeBiggerThanStorage = 6442450944L;

        private readonly FileStorage _sut;
        private readonly FileData _testFile;

        public FileStorageTests()
        {
            if (Directory.Exists(TestFolderPath))
            {
                Directory.Delete(TestFolderPath, true);
            }

            _sut = new FileStorage(TestFolderPath);
            _testFile = new FileData("https://example.com/video.mp4", "testFile", Int64.MaxValue, true);
        }

        [Fact]
        public void FileStorageConstructor_CreatesDirectory_WhenItNotExists()
        {
            //Arrange
            string newDirectoryPath = "newDirectory";

            if (Directory.Exists(newDirectoryPath))
            {
                Directory.Delete(newDirectoryPath, true);
            }

            // Act
            var fileStorage = new FileStorage(newDirectoryPath);

            // Assert
            Assert.Equal(newDirectoryPath, fileStorage.FolderPath);
            Assert.True(Directory.Exists(newDirectoryPath));
        }

        [Fact]
        public async Task WaitForDownloadQueueIfSizeFitAsync_ShouldReturnSizeLimitExceeded_WhenFileTooBig()
        {
            // Act
            DownloadResult result = await _sut.WaitForDownloadQueueIfSizeFitAsync(SizeBiggerThanStorage, _testFile);

            // Assert
            Assert.Equal(DownloadResult.SizeLimitExceeded, result);
        }

        [Fact]
        public async Task WaitForDownloadQueueIfSizeFitAsync_ShouldReturnQueueIsFull_WhenQueueIsFull()
        {
            const int MaxFilesInQueue = 10;

            // Act
            DownloadResult result = DownloadResult.Ok;

            _sut.AddSizeIfFileWasUntracked(_testFile, SizeBiggerThanStorage);

            for (int i = 0; i < MaxFilesInQueue; i++)
            {
                Task.Run(async () => await _sut.WaitForDownloadQueueIfSizeFitAsync(CorrectFileSizeInBytes, _testFile));
            }

            await Task.Delay(1000);
            result = await _sut.WaitForDownloadQueueIfSizeFitAsync(CorrectFileSizeInBytes, _testFile);
            
            // Assert
            Assert.Equal(DownloadResult.QueueIsFull, result);
        }

        [Fact]
        public async Task WaitForDownloadQueueIfSizeFitAsync_ShouldReturnOk_WhenFileFits()
        {
            // Act
            DownloadResult result = await _sut.WaitForDownloadQueueIfSizeFitAsync(CorrectFileSizeInBytes, _testFile);

            // Assert
            Assert.Equal(DownloadResult.Ok, result);
        }

        [Fact]
        public async Task DeleteAllFilesByName_ShouldDeleteAllFiles()
        {
            // Arrange
            string filenameStartsWith = "testFileName";
            string path = Path.Combine(TestFolderPath, filenameStartsWith);
            string pathToFileWithPostfix = $"{path}{GlobalConstants.TempPostfix}";
            string pathWithExtension = Path.ChangeExtension(path, ".3gp");
            string pathWithExtensionToFileWithPostfix = Path.ChangeExtension(pathToFileWithPostfix, ".mkv");
            string pathWithTwoExtensions = $"{pathWithExtensionToFileWithPostfix}.temp";

            string context = "test content";
            File.WriteAllText(path, context);
            File.WriteAllText(pathToFileWithPostfix, context);
            File.WriteAllText(pathWithExtension, context);
            File.WriteAllText(pathWithExtensionToFileWithPostfix, context);
            File.WriteAllText(pathWithTwoExtensions, context);

            // Act
            _sut.DeleteAllFilesByName(path);
            await Task.Delay(10);

            // Assert
            Assert.False(File.Exists(path));
            Assert.False(File.Exists(pathToFileWithPostfix));
            Assert.False(File.Exists(pathWithExtension));
            Assert.False(File.Exists(pathWithExtensionToFileWithPostfix));
            Assert.False(File.Exists(pathWithTwoExtensions));

            //Cleanup
            Cleanup.DeleteFileIfItExists(path);
            Cleanup.DeleteFileIfItExists(pathToFileWithPostfix);
            Cleanup.DeleteFileIfItExists(pathWithExtension);
            Cleanup.DeleteFileIfItExists(pathWithExtensionToFileWithPostfix);
            Cleanup.DeleteFileIfItExists(pathWithTwoExtensions);
        }
    }
}