using GlobalUtils;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace DownloadUtilsApi.Utils
{
    internal static class ProcessUtils
    {
        public static async Task<(string errors, string output)> ExecuteProcessWithOutputAsync(string path, string command)
        {
            var process = CreateProcessWithOutput(path, command);
            process.Start();

            string errors = await process.StandardError.ReadToEndAsync();
            string output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();
            return (errors, output);
        }

        public static async Task<string> ExecuteProcessAsync(string path, string command)
        {
            var process = CreateProcess(path, command);
            process.Start();

            string errors = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();
            return errors;
        }

        private static Process CreateProcessWithOutput(string appPath, string command)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = GetProcessFileName(appPath),
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
                Arguments = GetProcessStartArguments(appPath, command)
            };

            var process = new Process
            {
                StartInfo = processStartInfo
            };

            return process;
        }

        private static Process CreateProcess(string appPath, string command)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = GetProcessFileName(appPath),
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                StandardErrorEncoding = Encoding.UTF8,
                Arguments = GetProcessStartArguments(appPath, command)
            };

            var process = new Process
            {
                StartInfo = processStartInfo
            };

            return process;
        }

        private static string GetProcessFileName(string appPath)
        {
            const string CmdExe = "cmd.exe";

            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) 
                ? CmdExe 
                : appPath;
        }

        private static string GetProcessStartArguments(string appPath, string command)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) 
                ? String.Format(MessagesText.Formats.WindowsCmdFormat, appPath, command) 
                : command;
        }
    }
}