using DownloadUtilsApi.DependencyInjection.ProcessExecuters;
using DownloadUtilsApi.FFmpeg.Handlers;
using GlobalUtils;
using Moq;
using UnitTests.TestSetups;

namespace UnitTests.DownloadUtilsApi.FFmpeg.Handlers
{
    public class FFmpegResponceHandlerTests
    {
        private const string PathToFileWithSupportedCodecAndExtension = "supportedCodecCorrectExtension";
        private const string PathToFileWithSupportedCodecAndWrongExtension = "supportedCodecWrongExtension";
        private const string PathToFileWithUnsupportedCodec = "unsupportedCodec";

        private readonly FFmpegResponceHandler _sut;
        private readonly Mock<IRecoderProcessExecuter> _recoderProcessExecuterMock;
        private readonly Mock<IFileInspectorProcessExecuter> _inspectorProcessExecuterMock;

        public static readonly IReadOnlyCollection<object[]> _allCorrectCodecs = TestGlobalConstants.AllCorrectCodecs;

        public FFmpegResponceHandlerTests()
        {
            _recoderProcessExecuterMock = new Mock<IRecoderProcessExecuter>();
            _inspectorProcessExecuterMock = new Mock<IFileInspectorProcessExecuter>();

            _recoderProcessExecuterMock.SetupAllMethods();

            _sut = new FFmpegResponceHandler(_recoderProcessExecuterMock.Object, _inspectorProcessExecuterMock.Object);
        }

        [Theory]
        [MemberData(nameof(_allCorrectCodecs))]
        public async Task RecodeVideoIfNeededAsync_ShouldNotRecode_WhenCodecSupportedAndCorrectExtension(string supportedCodec, string correctExtension)
        {
            // Arrange
            ArrangePaths(PathToFileWithSupportedCodecAndExtension, correctExtension, out string path, out string pathWithPostfix);

            _inspectorProcessExecuterMock.SetupGetCodecAsync(supportedCodec);

            // Act
            string resultExtension = await _sut.RecodeVideoIfNeededAsync(path);

            // Assert
            Assert.Equal(correctExtension, resultExtension);
            _recoderProcessExecuterMock.VerifyNoOtherCalls();
            Assert.True(File.Exists(path));
            Assert.False(File.Exists(pathWithPostfix));

            //Cleanup
            string pathWithNewExtension = Path.ChangeExtension(path, resultExtension);
            string tempPathWithNewExtension = Path.ChangeExtension(pathWithPostfix, resultExtension);
            Cleanup.DeleteFileIfItExists(path);
            Cleanup.DeleteFileIfItExists(pathWithPostfix);
            Cleanup.DeleteFileIfItExists(pathWithNewExtension);
            Cleanup.DeleteFileIfItExists(tempPathWithNewExtension);
        }

        [Theory]
        [InlineData("")]
        [InlineData(".video")]
        [InlineData(".unknown_video")]
        [InlineData(".3gp")]
        [InlineData(VideoParameters.Extensions.Webm)]
        public async Task RecodeVideoIfNeededAsync_ShouldChangeExtension_WhenCodecSupportedAndWrongExtension(string notMp4Extension)
        {
            // Arrange
            ArrangePaths(PathToFileWithSupportedCodecAndWrongExtension, notMp4Extension, out string path, out string pathWithPostfix);

            _inspectorProcessExecuterMock.SetupGetCodecAsync(VideoParameters.Codecs.H264);

            // Act
            string resultExtension = await _sut.RecodeVideoIfNeededAsync(path);

            // Assert
            Assert.Equal(VideoParameters.Extensions.Mp4, resultExtension);
            _recoderProcessExecuterMock.VerifyChangeExtensionAsync();
            Assert.True(File.Exists(path));
            Assert.False(File.Exists(pathWithPostfix));

            // Cleanup
            string pathWithNewExtension = Path.ChangeExtension(path, resultExtension);
            string tempPathWithNewExtension = Path.ChangeExtension(pathWithPostfix, resultExtension);
            Cleanup.DeleteFileIfItExists(path);
            Cleanup.DeleteFileIfItExists(pathWithPostfix);
            Cleanup.DeleteFileIfItExists(pathWithNewExtension);
            Cleanup.DeleteFileIfItExists(tempPathWithNewExtension);
        }

        [Theory]
        [InlineData("unsupportedCodec")]
        [InlineData("")]
        public async Task RecodeVideoIfNeededAsync_ShouldRecode_WhenCodecNotSupported(string codec)
        {
            // Arrange
            ArrangePaths(PathToFileWithUnsupportedCodec, VideoParameters.Extensions.Mp4, out string path, out string pathWithPostfix);

            _inspectorProcessExecuterMock.SetupGetCodecAsync(codec);

            // Act
            string resultExtension = await _sut.RecodeVideoIfNeededAsync(path);

            // Assert
            Assert.Equal(VideoParameters.Extensions.Mp4, resultExtension);
            _recoderProcessExecuterMock.VerifyRecodeToH264Async();
            Assert.True(File.Exists(path));
            Assert.False(File.Exists(pathWithPostfix));

            // Cleanup
            string pathWithNewExtension = Path.ChangeExtension(path, resultExtension);
            string tempPathWithNewExtension = Path.ChangeExtension(pathWithPostfix, resultExtension);
            Cleanup.DeleteFileIfItExists(path);
            Cleanup.DeleteFileIfItExists(pathWithPostfix);
            Cleanup.DeleteFileIfItExists(pathWithNewExtension);
            Cleanup.DeleteFileIfItExists(tempPathWithNewExtension);
        }

        private static void ArrangePaths(string pathWithoutExtension, string extension, out string path, out string pathWithPostfix)
        {
            path = Path.ChangeExtension(pathWithoutExtension, extension);
            pathWithPostfix = GetPathWithPostfix(pathWithoutExtension, extension);
            File.WriteAllText(path, "text");
        }

        private static string GetPathWithPostfix(string pathWithoutExtension, string extension)
        {
            string pathWithoutExtensionWithPostfix = $"{pathWithoutExtension}{GlobalConstants.TempPostfix}";
            string pathWithPostfix = Path.ChangeExtension(pathWithoutExtensionWithPostfix, extension);
            return pathWithPostfix;
        }
    }
}