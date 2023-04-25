using System.Collections.Immutable;

namespace GlobalUtils
{
    public static class VideoParameters
    {        
        public static readonly IImmutableDictionary<string, string> RequiredExtensionsBySupportedCodecs;

        private static readonly Dictionary<string, string> _requiredExtensionsBySupportedCodecs = new()
        {
            { Codecs.H264, Extensions.Mp4 },
            { Codecs.Avc, Extensions.Mp4 },
            { Codecs.Mpeg4, Extensions.Mp4 },
            { Codecs.VP9, Extensions.Webm },
            { Codecs.VP8, Extensions.Webm }
        };
        
        static VideoParameters()
        {
            RequiredExtensionsBySupportedCodecs = _requiredExtensionsBySupportedCodecs.ToImmutableDictionary();
        }

        public static class Codecs
        {
            public const string VP8 = "vp8";
            public const string VP9 = "vp9";
            public const string Avc = "AVC";
            public const string H264 = "h264";
            public const string Mpeg4 = "MP4";
        }

        public static class Extensions
        {
            public const string Webm = ".webm";
            public const string Mp4 = ".mp4";
        }

    }
}