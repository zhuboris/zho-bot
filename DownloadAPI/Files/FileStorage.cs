using GlobalUtils;
using Serilog;
using System.Collections.Concurrent;

namespace DownloadAPI.Files
{
    internal class FileStorage
    {
        private const long SizeInBytes = 5368709120L; // 5 GB
        private const long MaxDownloableFileSizeInBytes = 104857600L; // 100 MB
        private const int MaxFilesInQueue = 10;
        private const int Delay = 10000;
        private const int SpaceMyltiplyForRecoding = 2;

        private long _freeDriveMemory = SizeInBytes;
        private readonly ConcurrentQueue<FileData> _filesInQueue = new();

        public FileStorage(string folderPath)
        {
            FolderPath = folderPath;
            CreateFolderIfMissing(FolderPath);
        }

        public string FolderPath { get; }

        public async Task<DownloadResult> WaitForDownloadQueueIfSizeFitAsync(long fileSizeInBytes, FileData file)
        {
            if (fileSizeInBytes > MaxDownloableFileSizeInBytes)
                return DownloadResult.SizeLimitExceeded;

            DownloadResult result = DownloadResult.Ok;

            fileSizeInBytes *= SpaceMyltiplyForRecoding;

            if (fileSizeInBytes > _freeDriveMemory)
            {
                result = await TryWaitInQueueAsync(fileSizeInBytes, file);
            }

            DecreaseFreeDriveMemory(fileSizeInBytes);
            return result;
        }

        public void DeleteAllFilesByName(string filePath)
        {
            Task.Run(() => 
            {
                var filePathsToDelete = GetPathsToDeleteFiles(filePath);

                if (File.Exists(filePath))
                    filePathsToDelete = filePathsToDelete.Append(filePath);

                DeleteFilesAsync(filePathsToDelete);
            });

            var size = GetFileSize(filePath) * SpaceMyltiplyForRecoding;
            IncreaseFreeDriveMemory(size);
        }

        public void AddSizeIfFileWasUntracked(FileData file, long sizeInBytes)
        {
            if (file.ShouldSkipSizeCheck)
            {
                sizeInBytes *= SpaceMyltiplyForRecoding;
                DecreaseFreeDriveMemory(sizeInBytes);
            }
        }

        private static IEnumerable<string> GetPathsToDeleteFiles(string filePath)
        {
            string? fileName = Path.GetFileName(filePath);
            string? folderPath = Path.GetDirectoryName(filePath);

            if (String.IsNullOrEmpty(fileName) || String.IsNullOrEmpty(folderPath))
                return Enumerable.Empty<string>();

            fileName = Path.ChangeExtension(fileName, null);
            string searchPattern = String.Format(MessagesText.Formats.SearchNamePattern, fileName);
            return Directory.EnumerateFiles(folderPath, searchPattern);
        }

        private static async Task DeleteFilesAsync(IEnumerable<string> filePaths)
        {
            foreach (string path in filePaths)
            {
                 await TryDeleteFileTillSuccessAsync(path);
            }
        }

        private static async Task TryDeleteFileTillSuccessAsync(string path)
        {
            bool wasNotDeleted = true;

            while (wasNotDeleted)
            {
                try
                {
                    File.Delete(path);
                    wasNotDeleted = false;
                }
                catch (IOException)
                {
                    await Task.Delay(Delay);
                }
            }
        }

        private void CreateFolderIfMissing(string folderPath)
        {
            if (Directory.Exists(FolderPath) == false)
                Directory.CreateDirectory(folderPath);
        }

        private async Task<DownloadResult> TryWaitInQueueAsync(long fileSizeInBytes, FileData file)
        {
            DownloadResult isFileEnqueued = TryEnqueue(file);

            if (isFileEnqueued.IsBadResult())
                return isFileEnqueued;

            await WaitForPeekAsync(file);
            await WaitForFreeMemoryAsync(fileSizeInBytes);
            return DownloadResult.Ok;
        }

        private DownloadResult TryEnqueue(FileData file)
        {
            if (_filesInQueue.Count < MaxFilesInQueue)
            {
                EnqueueFile(file);
                return DownloadResult.Ok;
            }

            Log.Error(MessagesText.Formats.DiscordLogTemplate, nameof(TryEnqueue), MessagesText.Info.QueueIsFull);
            return DownloadResult.QueueIsFull;
        }

        private void EnqueueFile(FileData file)
        {
            if (_filesInQueue.IsEmpty)
                Log.Warning(MessagesText.Formats.DiscordLogTemplate, nameof(TryEnqueue), MessagesText.Info.StorageIsFull);

            _filesInQueue.Enqueue(file);
        }

        private async Task WaitForPeekAsync(FileData file)
        {
            FileData? peek;

            do
            {
                await Task.Delay(Delay);
                _filesInQueue.TryPeek(out peek);
            } while (file != peek);

            _filesInQueue.TryDequeue(out _);
        }

        private async Task WaitForFreeMemoryAsync(long neededMemory)
        {
            while (neededMemory > _freeDriveMemory)
            {
                await Task.Delay(Delay);
            }
        }

        private void IncreaseFreeDriveMemory(long releasedMemory)
        {
            Interlocked.Add(ref _freeDriveMemory, - releasedMemory);
        }

        private void DecreaseFreeDriveMemory(long sizeInBytes)
        {
            long allocatedMemory = -sizeInBytes;
            Interlocked.Add(ref _freeDriveMemory, allocatedMemory);
        }

        private long GetFileSize(string filePath)
        {
            if (File.Exists(filePath) == false)
                return 0;

            FileInfo fileInfo = new(filePath);
            return fileInfo.Length;
        }
    }
}