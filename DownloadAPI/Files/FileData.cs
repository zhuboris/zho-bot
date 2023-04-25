using GlobalUtils;

namespace DownloadAPI.Files
{
    internal record FileData(string InputedUrl, string PathWithoutExtension, long MaxSize, bool ShouldSkipSizeCheck = false)
    {
        private string? _pathWithExtension;

        public string PathWithExtension => _pathWithExtension?? throw new NullReferenceException(MessagesText.Errors.PathMustBeSet);

        public DownloadResult IsTypeCorrect(string? type, string shouldStartsWith)
        {
            if (String.IsNullOrWhiteSpace(type))
                return DownloadResult.InvalidInput;

            return type.StartsWith(shouldStartsWith, StringComparison.OrdinalIgnoreCase) ? DownloadResult.Ok : DownloadResult.IncorrectFileType;
        }

        public void SetPathWithExtension(string extension)
        {
            _pathWithExtension = Path.ChangeExtension(PathWithoutExtension, extension);
        }

        public bool TryGetCurrentSize(out long sizeInBytes)
        {
            sizeInBytes = 0L;
            bool canGet = File.Exists(_pathWithExtension);

            if (canGet)
            {
                var fileInfo = new FileInfo(PathWithExtension);
                sizeInBytes = fileInfo.Length;
            }
                        
            return canGet;
        }
    }
}