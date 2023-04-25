using DownloadUtilsApi.DependencyInjection.ProcessExecuters;
using Moq;

namespace UnitTests.TestSetups
{
    internal static class MockIRecoderProcessExecuterExtensions
    {
        public static void SetupAllMethods(this Mock<IRecoderProcessExecuter> mock)
        {
            mock.SetupRecodeToH264Async()
                .SetupChangeExtensionAsync();
        }

        public static void VerifyChangeExtensionAsync(this Mock<IRecoderProcessExecuter> mock)
        {
            mock.Verify(executer => executer.ChangeExtensionAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        public static void VerifyRecodeToH264Async(this Mock<IRecoderProcessExecuter> mock)
        {
            mock.Verify(executer => executer.RecodeToH264Async(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        private static Mock<IRecoderProcessExecuter> SetupRecodeToH264Async(this Mock<IRecoderProcessExecuter> mock)
        {
            mock.Setup(executer => executer.RecodeToH264Async(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((string inputPath, string outputPath) =>
            {
                File.Copy(inputPath, outputPath);
                return String.Empty;
            });

            return mock;
        }

        private static Mock<IRecoderProcessExecuter> SetupChangeExtensionAsync(this Mock<IRecoderProcessExecuter> mock)
        {
            mock.Setup(executer => executer.ChangeExtensionAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((string inputPath, string outputPath) =>
            {
                File.Copy(inputPath, outputPath);
                return String.Empty;
            });

            return mock;
        }
    }
}