using DownloadUtilsApi.DependencyInjection;
using DownloadUtilsApi.DependencyInjection.ProcessExecuters;
using DownloadUtilsApi.DependencyInjection.ResponceHandlers;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.TestSetups;

namespace UnitTests.DownloadUtilsApi.DependencyInjection
{
    public class ServiceFactoryTests
    {
        [Fact]
        public void AddDownloadUtilsServices_ShouldAddRequiredServices()
        {
            // Arrange
            IServiceCollection services = new ServiceCollection();

            // Act
            services.AddDownloadUtilsServices();

            // Assert
            services.AssertThatContainsService(typeof(IDownloaderResponceHandler))
                .AssertThatContainsService(typeof(IRecoderResponceHandler))
                .AssertThatContainsService(typeof(IDownloaderProcessExecuter))
                .AssertThatContainsService(typeof(IFileInspectorProcessExecuter))
                .AssertThatContainsService(typeof(IRecoderProcessExecuter));
        }        
    }
}