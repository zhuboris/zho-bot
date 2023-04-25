using DownloadUtilsApi.DependencyInjection.ProcessExecuters;
using Moq;

namespace UnitTests.TestSetups
{
    internal static class MockIDownloaderProcessExecuterExtensions
    {
        public static void SetupAllMethods(this Mock<IDownloaderProcessExecuter> mock)
        {
            mock.SetupGetSizeAsync()
                .SetupGetFilenameAsync()
                .SetupDownloadAsync()
                .SetupDownloadAnySizeAsync();
        }

        private static Mock<IDownloaderProcessExecuter> SetupGetSizeAsync(this Mock<IDownloaderProcessExecuter> mock)
        {
            mock.Setup(executer => executer.GetSizeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .ReturnsAsync((string url, string _, long maxSize) =>
                {
                    return url switch
                    {
                        TestGlobalConstants.InvalidUrl => (TestGlobalConstants.OtherError, string.Empty),
                        TestGlobalConstants.UrlOfOversizedVideo => (TestGlobalConstants.FormatError, string.Empty),
                        _ => (string.Empty, (maxSize - 1).ToString()),
                    };
                });

            return mock;
        }

        private static Mock<IDownloaderProcessExecuter> SetupGetFilenameAsync(this Mock<IDownloaderProcessExecuter> mock)
        {
            mock.Setup(executer => executer.GetFilenameAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .ReturnsAsync((string url, string _, long _) =>
                {
                    return url switch
                    {
                        TestGlobalConstants.InvalidUrl => (TestGlobalConstants.OtherError, string.Empty),
                        TestGlobalConstants.UrlOfOversizedVideo => (TestGlobalConstants.FormatError, string.Empty),
                        _ => (string.Empty, $"{TestGlobalConstants.TestPath}{TestGlobalConstants.TestExtension}"),
                    };
                });

            return mock;
        }

        private static Mock<IDownloaderProcessExecuter> SetupDownloadAsync(this Mock<IDownloaderProcessExecuter> mock)
        {
            mock.Setup(executer => executer.DownloadAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .ReturnsAsync((string url, string path, long _) =>
                {
                    switch(url)
                    {
                        case TestGlobalConstants.InvalidUrl: return TestGlobalConstants.OtherError;
                        case TestGlobalConstants.UrlOfOversizedVideo: return TestGlobalConstants.FormatError;
                    }

                    using (File.Create(path)) {};
                    return String.Empty;
                });

            return mock;
        }

        private static Mock<IDownloaderProcessExecuter> SetupDownloadAnySizeAsync(this Mock<IDownloaderProcessExecuter> mock)
        {
            mock.Setup(executer => executer.DownloadAnySizeAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((string url, string path) =>
                {
                    if (url == TestGlobalConstants.InvalidUrl)
                        return TestGlobalConstants.OtherError;

                    using (File.Create(path)) { };
                    return string.Empty;
                });

            return mock;
        }
    }
}