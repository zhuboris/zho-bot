using DownloadUtilsApi.DependencyInjection.ProcessExecuters;
using DownloadUtilsApi.Utils;
using GlobalUtils;

namespace DownloadUtilsApi.FFmpeg
{
    internal class FFmpegProcessExecuter : IRecoderProcessExecuter
    {
        public Task<string> RecodeToH264Async(string inputPath, string outputPath)
        {
            string command = GetCommandToRecodeToH264(inputPath, outputPath);
            return ProcessUtils.ExecuteProcessAsync(Paths.FFmpeg, command);           
        }

        public Task<string> ChangeExtensionAsync(string inputPath, string outputPath)
        {
            string command = GetCommandToChangeExtension(inputPath, outputPath);
            return ProcessUtils.ExecuteProcessAsync(Paths.FFmpeg, command);
        }

        private string GetCommandToRecodeToH264(string inputPath, string outputPath)
        {
            string options = $"{Options.GetInput(inputPath)} {Options.GetVideoCodec()}";
            return $"{options} {outputPath}";
        }

        private string GetCommandToChangeExtension(string inputPath, string outputPath)
        {
            string options = $"{Options.GetInput(inputPath)}";
            return $"{options} {outputPath}";
        }
    }
}