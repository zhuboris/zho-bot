using GlobalUtils;

namespace DownloadUtilsApi
{
    internal static class OptionsValues
    {
        public static class YtDlp
        {
            public static class Commands
            {
                public const string ChangePath = "-o";
                public const string FFmpegLocation = "--ffmpeg-location";
                public const string SetFormat = "-f";
                public const string SetMaxNumberOfVideos = "-I";
                public const string GetInfo = "-O";              
            }

            public static class Values
            {
                public const float VideoProportion = 0.75f;
                public const float AudioProportion = 0.25f;

                public const string FileNameOption = "filename";
                public const int MaxNumberOfVideos = 1;

                private const string BestVideoWithAudio = "b";
                private const string BestVideo = "bv*";
                private const string BestAudio = "ba";
                private const string Filesize = "filesize";
                private const string FilesizeApprox = "filesize_approx";
                private const string MegaBytes = "M";
                private const string Height = "height";
                private const string LessThen = "<";
                private const string IsMoreOrEquial = ">=";
                private const string Plus = "+";
                private const string Or = "/";

                private const int MinResolution = 240;
                public static string SizeOption => $"%({Filesize},{FilesizeApprox})s";

                public static string GetFormatOptionValue(long maxSizeInBytes)
                {
                    float maxSizeInMb = maxSizeInBytes.ConvertToMb();

                    string videoHeightFormat = $"[{Height}{IsMoreOrEquial}{MinResolution}]";
                    float videoMaxSize = maxSizeInMb * VideoProportion;
                    float audioMaxSize = maxSizeInMb * AudioProportion;

                    return $"\"{BestVideo}[{Filesize}{LessThen}{videoMaxSize}{MegaBytes}]{videoHeightFormat}" +
                    $" {Plus} {BestAudio}[{Filesize}{LessThen}{audioMaxSize}{MegaBytes}]" +
                    $" {Or} {BestVideoWithAudio}[{Filesize}{LessThen}{maxSizeInMb}{MegaBytes}]{videoHeightFormat}" +
                    $" {Or} {BestVideoWithAudio}[{FilesizeApprox}{LessThen}{maxSizeInMb}{MegaBytes}]{videoHeightFormat}\"";
                }
            }
        }

        public static class FFmpeg
        {
            public static class Commands
            {
                public const string Input = "-i";
                public const string VideoCodec = "-c:v";
            }

            public static class Values
            {
                public const string H264Codec = "libx264";
            }
        }

        public static class FFprobe
        {
            public static class Commands
            {
                public const string LoggingLevelSettings = "-v";
                public const string SelectStreams = "-select_streams";
                public const string SetOutputParams = "-show_entries";
                public const string SetOutputFormat = "-of";
            }

            public static class Values
            {
                public const string ErrorLevel = "error";

                private const string VideoStream = "v:";
                private const string Streams = "stream=";
                private const string CodecName = "codec_name";
                private const string DefaultOutput = "default=";
                private const string DontPrintWrappers = "noprint_wrappers=";
                private const string DontShowKeys = "nokey=";
                private const int FirstIndex = 0;
                private const int Enabled = 1;

                public static string FirstVideoStream => $"{VideoStream}{FirstIndex}";
                public static string OutputParams => $"{Streams}{CodecName}";
                public static string OutputFormat => $"{DefaultOutput}{DontPrintWrappers}{Enabled}:{DontShowKeys}{Enabled}";
            }
        }
    }
}
