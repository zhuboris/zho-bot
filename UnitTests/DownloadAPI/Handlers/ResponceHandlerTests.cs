using DownloadAPI;
using DownloadAPI.Files;
using DownloadAPI.Handlers;
using GlobalUtils;

namespace UnitTests.DownloadAPI.Handlers
{
    public class ResponceHandlerTests
    {
        private const string CorrectUrl = "https://www.youtube.com/watch?v=xmvWCl7ChqQ";

        private readonly FileStorage _storage;

        public ResponceHandlerTests()
        {
            _storage = new("somePath");
        }

        [Fact]
        public async Task GetResponceToDownloadAsync_ShouldReturnPathAndEmptyErrorString_WhenUrlIsCorrectAndSuccessfullyDownload()
        {
            //Act
            var (errorText, filePath) = await ResponceHandlerUtils.GetResponceToDownloadAsync(CorrectUrl, Int64.MaxValue, _storage, SupportedTypes.Video, GetDownloadTaskWithOkResult);
            
            //Assert
            Assert.Empty(errorText);
            Assert.NotEmpty(filePath);
            Assert.Equal(SupportedTypes.Video.DefaultExtension(), Path.GetExtension(filePath));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("awdjpawd")]
        [InlineData("www.youtube.com/watch?v=xmvWCl7ChqQ")]
        public async Task GetResponceToDownloadAsync_ShouldReturnErrorAndEmtpyPath_WhenUrlInvalid(string? invalidUrl)
        {
            //Act
            var (errorText, filePath) = await ResponceHandlerUtils.GetResponceToDownloadAsync(invalidUrl, Int64.MaxValue, _storage, SupportedTypes.Video, GetDownloadTaskWithOkResult);

            //Assert
            Assert.NotEmpty(errorText);
            Assert.Empty(filePath);
        }

        [Fact]
        public async Task GetResponceToDownloadAsync_ShouldReturnErrorAndPathPattern_WhenUrlIsCorrectButGetBadResultFromDownload()
        {
            //Act
            var (errorText, filePath) = await ResponceHandlerUtils.GetResponceToDownloadAsync(CorrectUrl, Int64.MaxValue, _storage, SupportedTypes.Video, GetDownloadTaskWithBadResult);

            //Assert
            Assert.NotEmpty(errorText);
            Assert.NotEmpty(filePath);
            Assert.Empty(Path.GetExtension(filePath));
            Assert.False(filePath.EndsWith(GlobalConstants.TempPostfix, StringComparison.OrdinalIgnoreCase));
        }

        private Task<DownloadResult> GetDownloadTaskWithOkResult(FileData file, FileStorage storage, SupportedTypes typeToDownload)
        {
            string extension = typeToDownload.DefaultExtension();
            file.SetPathWithExtension(extension);
            return Task.FromResult(DownloadResult.Ok);
        }

        private Task<DownloadResult> GetDownloadTaskWithBadResult(FileData file, FileStorage storage, SupportedTypes typeToDownload)
        {
            string extension = typeToDownload.DefaultExtension();
            file.SetPathWithExtension(extension);
            return Task.FromResult(DownloadResult.SizeLimitExceeded);
        }
    }
}