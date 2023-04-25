using DownloadAPI.Files;

namespace UnitTests.DownloadAPI.Files
{
    public class FileBuilderTests
    {
        [Fact]
        public void CreateFile_ShouldReturnFileDataWithValidProperties()
        {
            // Arrange
            string url = "https://example.com/video.mp4";
            string folderPath = "testFolder";
            long maxSize = 100000000L;

            // Act
            FileData fileData = FileBuilder.CreateFile(url, folderPath, maxSize);

            // Assert
            Assert.Equal(url, fileData.InputedUrl);
            Assert.Equal(maxSize, fileData.MaxSize);
            Assert.Contains(folderPath, fileData.PathWithoutExtension);
            Assert.False(Path.HasExtension(fileData.PathWithoutExtension));
        }
    }
}