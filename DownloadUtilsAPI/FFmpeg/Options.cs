using DownloadUtilsApi.Utils;

namespace DownloadUtilsApi.FFmpeg
{
    internal static class Options
    {
        public static string GetInput(string inputPath)
        {
            return OptionsUtils.GetOptionAsCommand(OptionsValues.FFmpeg.Commands.Input, inputPath);
        }

        public static string GetVideoCodec()
        {
            return OptionsUtils.GetOptionAsCommand(OptionsValues.FFmpeg.Commands.VideoCodec, OptionsValues.FFmpeg.Values.H264Codec);
        }
    }
}