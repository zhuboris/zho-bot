using GlobalUtils;

namespace DownloadAPI.Files
{
    internal enum SupportedTypes
    {
        Video,
        Gif
    }

    internal static class SupportedTypesExtension
    {
        public static string ExpectedMime(this SupportedTypes type)
        {
            return type switch
            {
                SupportedTypes.Video => FileTypes.ExpectedMime.Video,
                SupportedTypes.Gif => FileTypes.ExpectedMime.Gif,
                _ => throw new ArgumentException(MessagesText.Errors.UnexpectedType)
            };
        }

        public static string DefaultExtension(this SupportedTypes type)
        {
            return type switch
            {
                SupportedTypes.Video => FileTypes.DefaultExtentions.Video,
                SupportedTypes.Gif => FileTypes.DefaultExtentions.Gif,
                _ => throw new ArgumentException(MessagesText.Errors.UnexpectedType)
            };
        }
    }
}
