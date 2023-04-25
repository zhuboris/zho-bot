using DownloadUtilsApi.DependencyInjection.ProcessExecuters;
using DownloadUtilsApi.Utils;
using GlobalUtils;

namespace DownloadUtilsApi.FFprobe
{
    internal class FFprobeProcessExecuter : IFileInspectorProcessExecuter
    {
        public Task<(string errors, string output)> GetCodecAsync(string path)
        {
            string command = GetCommandToGetCodec(path);
            return ProcessUtils.ExecuteProcessWithOutputAsync(Paths.FFprobe, command);
        }

        private static string GetCommandToGetCodec(string path)
        {
            string options = $"{Options.GetErrorLevelSetiings()} {Options.GetStream()} {Options.GetOutputParams()} {Options.GetOutputFormat()}";
            return $"{options} {path}";
        }
    }
}