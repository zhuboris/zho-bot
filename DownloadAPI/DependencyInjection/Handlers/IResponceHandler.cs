namespace DownloadAPI.DependencyInjection.Handlers
{
    public interface IResponceHandler
    {
        public Task<(string errorText, string filePath)> SendResponceToDownloadVideoAsync(string? url, long uploadLimitInBytes);

        public Task<(string errorText, string filePath)> SendResponceToDownloadGifAsync(string? url, long uploadLimitInBytes);

        public void SendResponceToDelete(string filePath);
    }
}
