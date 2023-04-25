using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.TestSetups
{
    internal static class IServiceCollectionExtensions
    {
        public static IServiceCollection AssertThatContainsService(this IServiceCollection services, Type service)
        {
            Assert.Single(services
                .OfType<ServiceDescriptor>()
                .Where(descriptor => descriptor.ServiceType == service));

            return services;
        }
    }
}