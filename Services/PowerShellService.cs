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
                StandardInputEncoding = Encoding.Default,
                StandardOutputEncoding = Encoding.Default,
                StandardErrorEncoding = Encoding.Default,
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
                        // chcp 65001 is not needed if the stream encoding is handled correctly without BOM
                        await sw.WriteLineAsync(script);
                    }
                }

                await process.WaitForExitAsync();
                return process.ExitCode;
            }
        }
    }
}
