using Microsoft.Extensions.Configuration;

namespace GlobalUtils
{
    public static class TokenGetter
    {
        public static string? GetToken(string path, string key)
        {
            const string SettingsKey = "Settings";

            var config = new ConfigurationBuilder()
                .AddJsonFile(path)
                .AddEnvironmentVariables()
                .Build();

            var token = config.GetRequiredSection(SettingsKey)[key];
            return token;
        }
    }
}