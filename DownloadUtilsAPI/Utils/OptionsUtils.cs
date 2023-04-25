namespace DownloadUtilsApi.Utils
{
    internal static class OptionsUtils
    {
        public static string GetOptionAsCommand(string optionCommand, string optionValue)
        {
            return $"{optionCommand} {optionValue}";
        }
    }
}