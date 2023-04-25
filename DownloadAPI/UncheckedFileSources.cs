namespace DownloadAPI
{
    internal static class UncheckedFileSources
    {
        private const string TelegramUrl1 = "https://t.me/";
        private const string TelegramUrl2 = "http://t.me/";

        private static readonly List<string> _files = new() { TelegramUrl1, TelegramUrl2 };

        public static bool IsInUnckecked(string url)
        {
            foreach (var file in _files)
            {
                if (url.StartsWith(file, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
