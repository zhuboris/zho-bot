using DownloadUtilsApi.DependencyInjection.ProcessExecuters;
using Moq;

namespace UnitTests.TestSetups
{
    internal static class MockIFileInspectorProcessExecuterExtensions
    {
        public static void SetupGetCodecAsync(this Mock<IFileInspectorProcessExecuter> mock, string codec)
        {
            mock.Setup(executer => executer.GetCodecAsync(It.IsAny<string>()))
                .ReturnsAsync((string.Empty, codec));
        }
    }
}