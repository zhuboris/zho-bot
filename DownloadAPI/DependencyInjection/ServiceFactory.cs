using DownloadAPI.DependencyInjection.Handlers;
using DownloadAPI.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace DownloadAPI.DependencyInjection
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddDownloadServices(this IServiceCollection services)
        {
            return services.AddScoped<IResponceHandler, ResponceHandler>();
        }
    }
}