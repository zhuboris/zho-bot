using System.Runtime.InteropServices;

namespace GlobalUtils
{
    public static class Paths
    { 
        public const string VideoFolder = "video";
        public const string InviteBotUrl = "https://discord.com/api/oauth2/authorize?client_id=1089819594211987510&permissions=412317181952&scope=bot%20applications.commands";
        public const string GitUrl = "https://github.com/zhuboris/zho-bot";
    
        private const string YtDlpWindowsPath = "C:\\DownloadVideo\\yt-dlp.exe";
        private const string FFmpegWindowsPath = "C:\\DownloadVideo\\ffmpeg.exe";
        private const string FFprobeWindowsPath = "C:\\DownloadVideo\\ffprobe.exe"; 
        
        private const string YtDlpExecutableName = "yt-dlp";
        private const string FFmpegExecutableName = "ffmpeg";
        private const string FFprobeExecutableName = "ffprobe";

        static Paths()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                YtDlp = YtDlpWindowsPath;
                FFmpeg = FFmpegWindowsPath;
                FFprobe = FFprobeWindowsPath;
            }
            else
            {
                YtDlp = YtDlpExecutableName;
                FFmpeg = FFmpegExecutableName;
                FFprobe = FFprobeExecutableName;
            }
        }

        public static string YtDlp { get; }
        public static string FFmpeg { get; }
        public static string FFprobe { get; }

    }
}