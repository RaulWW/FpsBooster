using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text;

namespace FpsBooster.Services
{
    public class PowerShellService : IPowerShellService
    {
        public event Action<string>? OnOutputReceived;
        public event Action<string>? OnErrorReceived;

        public async Task<int> ExecuteCommandAsync(string script)
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = "-NoProfile -ExecutionPolicy Bypass -Command -",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardInputEncoding = new UTF8Encoding(false),
                StandardOutputEncoding = new UTF8Encoding(false),
                StandardErrorEncoding = new UTF8Encoding(false),
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using (var process = new Process { StartInfo = processInfo })
            {
                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                        OnOutputReceived?.Invoke(e.Data);
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                        OnErrorReceived?.Invoke(e.Data);
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                using (var sw = process.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        // Standard streams are already set to UTF8 in the ProcessStartInfo.
                        // Force PowerShell to use UTF8 for Console Output to match our stream encoding
                        await sw.WriteLineAsync("[Console]::OutputEncoding = [System.Text.Encoding]::UTF8");
                        await sw.WriteLineAsync("$OutputEncoding = [System.Text.Encoding]::UTF8");
                        
                        await sw.WriteLineAsync(script);
                    }
                }

                await process.WaitForExitAsync();
                return process.ExitCode;
            }
        }
    }
}
