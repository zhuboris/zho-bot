using AppConfigurator;
using DownloadAPI.DependencyInjection.Handlers;
using GlobalUtils;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.TestSetups;

namespace IntegrationTests
{
    public class ResponseHandlerTests
    {
        private const string UrlToSmallVideo = "https://youtu.be/DALfkiOu1vc";
        private const string UrlToNotSupportedCodeclVideo = "https://s3.amazonaws.com/x265.org/video/Tractor_500kbps_x265.mp4";
        private const string UrlToWrongExtensionVideo = "https://vk.com/video-159839157_456249415";
        private const string UrlToTooBigVideo = "https://www.youtube.com/watch?v=f9VbU_pCh8w";
        private const string UrlToSmallGif = "https://media4.giphy.com/media/JfDNFU1qOZna/giphy.gif?cid=ecf05e479p7aiksq4pex3b2zuhb0wsyk4b8q21h1dntqpyc9&rid=giphy.gif&ct=g";
        private const string UrlToTooBigGif = "https://img2.joyreactor.cc/pics/post/%D0%B0%D1%80%D0%B1%D1%83%D0%B7-%D0%B3%D0%B8%D1%84-%D0%B1%D0%BE%D0%BB%D1%8C%D1%88%D0%B0%D1%8F-%D0%B3%D0%B8%D1%84%D0%BA%D0%B0-4879139.gif"; //https://media3.giphy.com/media/YRtLgsajXrz1FNJ6oy/giphy.gif?cid=ecf05e47d6kiahumw3ti9rbgd05mt1pugw3rdwuv1cxdmot1&rid=giphy.gif&ct=g
        private const long StandartUploadLimitInBytes = 8388608L;

        private readonly IResponceHandler _sut;

        public ResponseHandlerTests()
        {
            IServiceProvider serviceProvider = ServicesConfigurator.GetServiceProvider();

            _sut = serviceProvider.GetRequiredService<IResponceHandler>();
        }

        [Fact]
        public async Task SendResponceToDelete_ShouldDeleteAllFilesByPAttern()
        {
            // Arrange
            string filenameStartsWith = "testFileName";
            string path = Path.Combine(Paths.VideoFolder, filenameStartsWith);
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
            _sut.SendResponceToDelete(path);
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

        [Fact]
        public async Task SendResponceToDownloadVideoAsync_ShouldDownload_WhenCorrectInputWithoutRecoding()
        {
            //Act
            var (errorText, filePath) = await _sut.SendResponceToDownloadVideoAsync(UrlToSmallVideo, StandartUploadLimitInBytes);

            //Assert
            Assert.Empty(errorText);
            Assert.True(File.Exists(filePath));
            Assert.Contains(Path.GetExtension(filePath), TestGlobalConstants.ExpectedVideoExtensions);
            Assert.True(new FileInfo(filePath).Length <= StandartUploadLimitInBytes);

            //Cleanup
            _sut.SendResponceToDelete(filePath);
        }

        [Fact]
        public async Task SendResponceToDownloadVideoAsync_ShouldDownloadAndRecode_WhenCorrectInputAndNotSupportedCodec()
        {
            //Act
            var (errorText, filePath) = await _sut.SendResponceToDownloadVideoAsync(UrlToNotSupportedCodeclVideo, StandartUploadLimitInBytes);

            //Assert
            Assert.Empty(errorText);
            Assert.True(File.Exists(filePath));
            Assert.Contains(Path.GetExtension(filePath), TestGlobalConstants.ExpectedVideoExtensions);
            Assert.True(new FileInfo(filePath).Length <= StandartUploadLimitInBytes);

            //Cleanup
            _sut.SendResponceToDelete(filePath);
        }

        [Fact]
        public async Task SendResponceToDownloadVideoAsync_ShouldDownloadAndChangeExtension_WhenCorrectInputWithSupportedCodecButWrongExtension()
        {
            //Act
            var (errorText, filePath) = await _sut.SendResponceToDownloadVideoAsync(UrlToWrongExtensionVideo, StandartUploadLimitInBytes);

            //Assert
            Assert.Empty(errorText);
            Assert.True(File.Exists(filePath));
            Assert.Contains(Path.GetExtension(filePath), TestGlobalConstants.ExpectedVideoExtensions);
            Assert.True(new FileInfo(filePath).Length <= StandartUploadLimitInBytes);

            //Cleanup
            _sut.SendResponceToDelete(filePath);
        }

        [Fact]
        public async Task SendResponceToDownloadVideoAsync_ShouldNotDownloadAndReturnError_WhenUrlIsValidButSizeTooBig()
        {
            //Act
            var (errorText, filePath) = await _sut.SendResponceToDownloadVideoAsync(UrlToTooBigVideo, StandartUploadLimitInBytes);

            //Assert
            Assert.Contains("File is too big", errorText, StringComparison.OrdinalIgnoreCase);
            Assert.NotEmpty(filePath);
            Assert.False(File.Exists(filePath));
            Assert.Empty(Path.GetExtension(filePath));

            //Cleanup
            _sut.SendResponceToDelete(filePath);
        }

        [Theory]
        [InlineData("")]
        [InlineData("asdfgh")]
        public async Task SendResponceToDownloadVideoAsync_ShouldNotDownloadAndReturnError_WhenUrlIsInvalid(string? invalidUrl)
        {
            //Act
            var (errorText, filePath) = await _sut.SendResponceToDownloadVideoAsync(invalidUrl, StandartUploadLimitInBytes);

            //Assert
            Assert.Equal(MessagesText.Errors.WrongInput, errorText, ignoreCase: true, ignoreLineEndingDifferences: true);
            Assert.Empty(filePath);

            //Cleanup
            _sut.SendResponceToDelete(filePath);
        }

        [Theory]
        [InlineData("https://www.nkj.ru/facts/47872/")]
        [InlineData("https://i0.wp.com/lifeo.ru/wp-content/uploads/hdon-33-min.jpg")]
        [InlineData("https://i.imgur.com/yAC9D2H.png")]
        [InlineData("https://www.educaciontrespuntocero.com/wp-content/uploads/2019/06/homer.gif")]
        public async Task SendResponceToDownloadVideoAsync_ShouldNotDownloadAndReturnError_WhenUrlIsValidButTypeIsWrong(string? wrongTypeUrl)
        {
            //Act
            var (errorText, filePath) = await _sut.SendResponceToDownloadVideoAsync(wrongTypeUrl, StandartUploadLimitInBytes);

            //Assert
            Assert.Contains("URL does not contain suitable file to download", errorText, StringComparison.OrdinalIgnoreCase);
            Assert.NotEmpty(filePath);
            Assert.False(File.Exists(filePath));
            Assert.Empty(Path.GetExtension(filePath));

            //Cleanup
            _sut.SendResponceToDelete(filePath);
        }

        [Fact]
        public async Task SendResponceToDownloadGifAsync_ShouldDownload_WhenUrlIsValidAndGifFitsSize()
        {
            //Act
            var (errorText, filePath) = await _sut.SendResponceToDownloadGifAsync(UrlToSmallGif, StandartUploadLimitInBytes);

            //Assert
            Assert.Empty(errorText);
            Assert.True(File.Exists(filePath));
            Assert.Equal(FileTypes.DefaultExtentions.Gif, Path.GetExtension(filePath));
            Assert.True(new FileInfo(filePath).Length <= StandartUploadLimitInBytes);

            //Cleanup
            _sut.SendResponceToDelete(filePath);
        }

        [Fact]
        public async Task SendResponceToDownloadGifAsync_ShouldNotDownloadAndReturnError_WhenUrlIsValidButSizeTooBig()
        {
            //Act
            var (errorText, filePath) = await _sut.SendResponceToDownloadGifAsync(UrlToTooBigGif, StandartUploadLimitInBytes);

            //Assert
            Assert.Contains("File is too big", errorText, StringComparison.OrdinalIgnoreCase);
            Assert.NotEmpty(filePath);
            Assert.False(File.Exists(filePath));
            Assert.Empty(Path.GetExtension(filePath));

            //Cleanup
            _sut.SendResponceToDelete(filePath);
        }

        [Theory]
        [InlineData("")]
        [InlineData("qwerty")]
        public async Task SendResponceToDownloadGifAsync_ShouldNotDownloadAndReturnError_WhenUrlIsInvalid(string? invalidUrl)
        {
            //Act
            var (errorText, filePath) = await _sut.SendResponceToDownloadGifAsync(invalidUrl, StandartUploadLimitInBytes);

            //Assert
            Assert.Equal(MessagesText.Errors.WrongInput, errorText, ignoreCase: true, ignoreLineEndingDifferences: true);
            Assert.Empty(filePath);

            //Cleanup
            _sut.SendResponceToDelete(filePath);
        }

        [Theory]
        [InlineData("https://www.nkj.ru/facts/47978/")]
        [InlineData("https://st23.styapokupayu.ru/ckeditor_assets/pictures/000/399/958_content.jpg")]
        [InlineData("https://www.anekdot.ru/i/caricatures/normal/22/1/26/1643206080.jpg")]
        [InlineData("https://youtu.be/r7QRDKEuk3Q")]
        public async Task SendResponceToDownloadGifAsync_ShouldNotDownloadAndReturnError_WhenUrlIsValidButTypeIsWrong(string? wrongTypeUrl)
        {
            //Act
            var (errorText, filePath) = await _sut.SendResponceToDownloadGifAsync(wrongTypeUrl, StandartUploadLimitInBytes);

            //Assert
            Assert.Equal(MessagesText.Errors.WrongType, errorText, ignoreCase: true, ignoreLineEndingDifferences: true);
            Assert.NotEmpty(filePath);
            Assert.False(File.Exists(filePath));
            Assert.Empty(Path.GetExtension(filePath));

            //Cleanup
            _sut.SendResponceToDelete(filePath);
        }
    }
}