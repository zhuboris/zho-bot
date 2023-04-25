using GlobalUtils;

namespace DownloadAPI.Files
{
    internal static class FileBuilder
    {
        public static FileData CreateFile(string url, string? folderPath, long maxSize)
        {
            string filePath = GetFilePath(folderPath);
            bool shouldSkipSizeCheck = UncheckedFileSources.IsInUnckecked(url);

            return new FileData(url, filePath, maxSize, shouldSkipSizeCheck);
        }

        private static string GetFilePath(string? folderPath)
        {
            folderPath ??= String.Empty;

            string fileName = GetUniqueRandomName(folderPath);
            return Path.Combine(folderPath, fileName);
        }

        private static string GetUniqueRandomName(string folderPath)
        {
            string fileName;

            do
            {
                fileName = Path.GetRandomFileName();
                fileName = Path.ChangeExtension(fileName, null);
            } while (WasFilenameReserved(folderPath, fileName));

            return fileName;
        }

        private static bool WasFilenameReserved(string folderPath, string fileName)
        {
            return (fileName.EndsWith(GlobalConstants.TempPostfix, StringComparison.OrdinalIgnoreCase)) || File.Exists(Path.Combine(folderPath, fileName));
        }
    }
}