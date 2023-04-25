namespace DownloadUtilsApi.DependencyInjection.ResponceHandlers
{
    public interface IRecoderResponceHandler
    {
        public Task<string> RecodeVideoIfNeededAsync(string path);        
    }
}