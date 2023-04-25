using DownloadUtilsApi.Utils;
using GlobalUtils;

namespace DownloadUtilsApi.YtDlp
{
    internal static class Options
    {
        public static string GetPath(string path)
        {
            string fullPath = GetPathWithUndefiniteExtension(path);
            string newPathCommand = OptionsUtils.GetOptionAsCommand(OptionsValues.YtDlp.Commands.ChangePath, fullPath);
            return newPathCommand;
        }

        public static string GetMaxNumberOfVideos()
        {
            string newPathCommand = OptionsUtils.GetOptionAsCommand(OptionsValues.YtDlp.Commands.SetMaxNumberOfVideos, OptionsValues.YtDlp.Values.MaxNumberOfVideos.ToString());
            return newPathCommand;
        }

        public static string GetFormat(long maxSize)
        {
            string formatCommand = OptionsUtils.GetOptionAsCommand(OptionsValues.YtDlp.Commands.SetFormat, OptionsValues.YtDlp.Values.GetFormatOptionValue(maxSize));
            return formatCommand;
        }

        public static string GetSize()
        {
            string formatCommand = OptionsUtils.GetOptionAsCommand(OptionsValues.YtDlp.Commands.GetInfo, OptionsValues.YtDlp.Values.SizeOption);
            return formatCommand;
        }

        public static string GetFileName()
        {
            string formatCommand = OptionsUtils.GetOptionAsCommand(OptionsValues.YtDlp.Commands.GetInfo, OptionsValues.YtDlp.Values.FileNameOption);
            return formatCommand;
        }

        private static string GetPathWithUndefiniteExtension(string path)
        {
            string fullPathWithoutExtention = FileUtils.GetFullPathWithoutExtention(path);
            string fullPath = $"{fullPathWithoutExtention}{GlobalConstants.AutodetectedExtensionForYtDlp}";
            return fullPath;
        }
    }
}