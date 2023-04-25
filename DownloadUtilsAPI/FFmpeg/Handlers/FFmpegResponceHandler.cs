using DownloadUtilsApi.DependencyInjection.ProcessExecuters;
using DownloadUtilsApi.DependencyInjection.ResponceHandlers;
using DownloadUtilsApi.Utils;
using GlobalUtils;

namespace DownloadUtilsApi.FFmpeg.Handlers
{
    public class FFmpegResponceHandler : IRecoderResponceHandler
    {
        private readonly IRecoderProcessExecuter _recoderProcessExecuter;
        private readonly IFileInspectorProcessExecuter _inspectorProcessExecuter;

        public FFmpegResponceHandler(IRecoderProcessExecuter recoderProcessExecuter, IFileInspectorProcessExecuter inspectorProcessExecuter)
        {
            _recoderProcessExecuter = recoderProcessExecuter;
            _inspectorProcessExecuter = inspectorProcessExecuter;
        }

        public async Task<string> RecodeVideoIfNeededAsync(string path)
        {
            var (_, codec) = await _inspectorProcessExecuter.GetCodecAsync(path);
            string detectedCodec = GetMatchingCodecFromSupported(codec);

            if (string.IsNullOrEmpty(detectedCodec))
            {
                await ChangeCodecToH264Async(path);
                return VideoParameters.RequiredExtensionsBySupportedCodecs[VideoParameters.Codecs.H264];
            }

            string? extension = Path.GetExtension(path);

            if (VideoParameters.RequiredExtensionsBySupportedCodecs[detectedCodec] != extension)
            {
                await ChangeExtensionAsync(path, extension);
                return VideoParameters.RequiredExtensionsBySupportedCodecs[detectedCodec];
            }

            return extension;
        }

        private string GetMatchingCodecFromSupported(string? codec)
        {
            foreach (string? supportedCodec in VideoParameters.RequiredExtensionsBySupportedCodecs.Keys)
            {
                if (codec is not null && codec.Contains(supportedCodec, StringComparison.OrdinalIgnoreCase))
                    return supportedCodec;
            }

            return string.Empty;
        }

        private async Task ChangeCodecToH264Async(string path)
        {
            string pathWithPostfix = FileUtils.RenameFileWithPostfix(path);
            string editedPath = Path.ChangeExtension(path, VideoParameters.Extensions.Mp4);

            var errors = await _recoderProcessExecuter.RecodeToH264Async(pathWithPostfix, editedPath);
            HandleErrors(pathWithPostfix, errors);
        }

        private async Task ChangeExtensionAsync(string path, string newExtension)
        {
            string pathWithPostfix = FileUtils.RenameFileWithPostfix(path);
            string editedPath = Path.ChangeExtension(path, newExtension);

            var errors = await _recoderProcessExecuter.ChangeExtensionAsync(pathWithPostfix, editedPath);
            HandleErrors(pathWithPostfix, errors);
        }

        private void HandleErrors(string pathWithPostfix, string errors)
        {
            if (errors.Contains(GlobalConstants.ErrorText, StringComparison.OrdinalIgnoreCase))
                CancelChanges(pathWithPostfix);
            else
                FileUtils.Delete(pathWithPostfix);
        }

        private void CancelChanges(string tempPath)
        {
            FileUtils.RenameFileBack(tempPath);
        }
    }
}