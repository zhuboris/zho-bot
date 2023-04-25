using DownloadUtilsApi.DependencyInjection.ResponceHandlers;
using GlobalUtils;
using Moq;

namespace UnitTests.TestSetups
{
    internal static class MockIRecoderResponceHandlerExtensions
    {
        public static Mock<IRecoderResponceHandler> SetupRecodeVideoIfNeededAsync(this Mock<IRecoderResponceHandler> mock)
        {            
            mock.Setup(recorder => recorder.RecodeVideoIfNeededAsync(It.IsAny<string>()))
                .ReturnsAsync((string path) =>
                {
                    string? extension = Path.GetExtension(path);

                    if (TestGlobalConstants.ExpectedVideoExtensions.Contains(extension) == false)
                    {
                        extension = FileTypes.DefaultExtentions.Video;
                        string newPath = Path.ChangeExtension(path, extension);
                        File.Move(path, newPath);
                    }

                    return extension;
                });

            return mock;
        }
    }
}