using DownloadUtilsApi.Utils;

namespace DownloadUtilsApi.FFprobe
{
    internal static class Options
    {
        public static string GetErrorLevelSetiings()
        {
            return OptionsUtils.GetOptionAsCommand(OptionsValues.FFprobe.Commands.LoggingLevelSettings, OptionsValues.FFprobe.Values.ErrorLevel);
        }

        public static string GetStream()
        {
            return OptionsUtils.GetOptionAsCommand(OptionsValues.FFprobe.Commands.SelectStreams, OptionsValues.FFprobe.Values.FirstVideoStream);
        }

        public static string GetOutputParams()
        {
            return OptionsUtils.GetOptionAsCommand(OptionsValues.FFprobe.Commands.SetOutputParams, OptionsValues.FFprobe.Values.OutputParams);
        }

        public static string GetOutputFormat()
        {
            return OptionsUtils.GetOptionAsCommand(OptionsValues.FFprobe.Commands.SetOutputFormat, OptionsValues.FFprobe.Values.OutputFormat);
        }
    }
}