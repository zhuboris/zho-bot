using DownloadAPI.DependencyInjection;
using DownloadAPI.DependencyInjection.Handlers;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.TestSetups;

namespace UnitTests.DownloadApi.DependencyInjection
{
    public class ServiceFactoryTests
    {
        [Fact]
        public void AddDownloadServices_ShouldAddRequiredServices()
        {
            // Arrange
            IServiceCollection services = new ServiceCollection();

            // Act
            services.AddDownloadServices();

            // Assert
            services.AssertThatContainsService(typeof(IResponceHandler));
        }        
    }
}