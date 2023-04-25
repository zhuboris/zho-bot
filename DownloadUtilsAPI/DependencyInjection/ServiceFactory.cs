using DownloadUtilsApi.DependencyInjection.ProcessExecuters;
using DownloadUtilsApi.DependencyInjection.ResponceHandlers;
using DownloadUtilsApi.FFmpeg;
using DownloadUtilsApi.FFmpeg.Handlers;
using DownloadUtilsApi.FFprobe;
using DownloadUtilsApi.YtDlp;
using DownloadUtilsApi.YtDlp.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace DownloadUtilsApi.DependencyInjection
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddDownloadUtilsServices(this IServiceCollection services)
        {
            return services.AddScoped<IRecoderResponceHandler, FFmpegResponceHandler>()
                .AddScoped<IDownloaderResponceHandler, YtDlpResponceHandler>()
                .AddScoped<IDownloaderProcessExecuter, YtDlpProcessExecuter>()
                .AddScoped<IFileInspectorProcessExecuter, FFprobeProcessExecuter>()
                .AddScoped<IRecoderProcessExecuter, FFmpegProcessExecuter>();
        }
    }
}