namespace DownloadUtilsApi.DependencyInjection.ProcessExecuters
{
    public interface IFileInspectorProcessExecuter
    {
        public Task<(string errors, string output)> GetCodecAsync(string path);
    }
}