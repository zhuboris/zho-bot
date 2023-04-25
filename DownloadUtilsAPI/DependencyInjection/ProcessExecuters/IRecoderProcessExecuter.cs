using DownloadUtilsApi.FFprobe;

namespace DownloadUtilsApi.DependencyInjection.ProcessExecuters
{
    public interface IRecoderProcessExecuter
    {
        public Task<string> RecodeToH264Async(string inputPath, string outputPath);

        public Task<string> ChangeExtensionAsync(string inputPath, string outputPath);
    }
}