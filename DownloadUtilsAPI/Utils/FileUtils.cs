using GlobalUtils;

namespace DownloadUtilsApi.Utils
{
    internal static class FileUtils
    {
        public static string GetFullPathWithoutExtention(string path)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
            string directoryName = Path.GetDirectoryName(path) ?? String.Empty;
            string absolutePathWithoutExtension = Path.Combine(directoryName, fileNameWithoutExtension);
            return absolutePathWithoutExtension;
        }

        public static string RenameFileWithPostfix(string path)
        {
            string newFilePath = GetFilenameWithAddedPostfix(path);
            return ChangePath(path, newFilePath);
        }

        public static string RenameFileBack(string pathWithPostfix)
        {
            string newFilePath = GetFilenameWithoutAddedPostfix(pathWithPostfix);
            return ChangePath(pathWithPostfix, newFilePath);
        }

        public static void Delete(string path)
        {
            Task.Run(() => File.Delete(path));
        }

        private static string ChangePath(string oldPath, string newPath)
        {
            DeleteFileIfAlreadyExist(newPath);
            File.Move(oldPath, newPath);
            return newPath;
        }

        private static string GetFilenameWithAddedPostfix(string path)
        {
            string directoryPath = Path.GetDirectoryName(path) ?? String.Empty;
            string oldFileName = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path);

            string newFileName = $"{oldFileName}{GlobalConstants.TempPostfix}{extension}";
            string newFilePath = Path.Combine(directoryPath, newFileName);
            return newFilePath;
        }

        private static string GetFilenameWithoutAddedPostfix(string path)
        {
            string directoryPath = Path.GetDirectoryName(path) ?? String.Empty;
            string oldFileName = Path.GetFileNameWithoutExtension(path);

            if (oldFileName.EndsWith(GlobalConstants.TempPostfix) == false)
                return path;

            int postfixBeginningIndex = oldFileName.Length - GlobalConstants.TempPostfix.Length;
            string nameWithoutPostfix = oldFileName.Remove(postfixBeginningIndex);
            
            string extension = Path.GetExtension(path);

            string newFileName = $"{nameWithoutPostfix}{extension}";
            string newFilePath = Path.Combine(directoryPath, newFileName);
            return newFilePath;
        }

        private static void DeleteFileIfAlreadyExist(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}