using System.Diagnostics;
using System.Threading.Tasks;

namespace FpsBooster.Services
{
    public class WindowsFeaturesInstaller
    {
        private readonly IPowerShellService _psService;

        public WindowsFeaturesInstaller(IPowerShellService? psService = null)
        {
            _psService = psService ?? new PowerShellService();
        }

        public async Task InstallVisualCppRuntimesAsync(IProgress<string> progress)
        {
            const string url = "https://us5-dl.techpowerup.com/files/EDfaqf03YoduxtDM8mz24w/1768404504/Visual-C-Runtimes-All-in-One-Dec-2025.zip";
            string tempPath = Path.GetTempPath();
            string zipPath = Path.Combine(tempPath, "vcpp_runtimes.zip");
            string extractPath = Path.Combine(tempPath, "vcpp_runtimes");

            try
            {
                progress.Report("Downloading Visual C++ Runtimes...");
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    using (var fs = new FileStream(zipPath, FileMode.Create))
                    {
                        await response.Content.CopyToAsync(fs);
                    }
                }

                progress.Report("Extracting files...");
                if (Directory.Exists(extractPath))
                    Directory.Delete(extractPath, true);
                
                System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);

                string batFile = Path.Combine(extractPath, "install_all.bat");
                if (File.Exists(batFile))
                {
                    progress.Report("Installing Runtimes (This may take a while)...");
                    var psi = new ProcessStartInfo
                    {
                        FileName = batFile,
                        Verb = "runas", // Ensure admin
                        UseShellExecute = true,
                        WorkingDirectory = extractPath,
                        CreateNoWindow = false 
                    };

                    using (var process = Process.Start(psi))
                    {
                        if (process != null)
                        {
                            await process.WaitForExitAsync();
                            progress.Report("Installation Completed!");
                        }
                        else
                        {
                            progress.Report("Error: Could not start the installation process.");
                        }
                    }
                }
                else
                {
                    progress.Report("Error: install_all.bat not found in the archive.");
                }
            }
            catch (Exception ex)
            {
                progress.Report($"Error: {ex.Message}");
            }
            finally
            {
                progress.Report("Cleaning up...");
                if (File.Exists(zipPath)) File.Delete(zipPath);
                if (Directory.Exists(extractPath)) Directory.Delete(extractPath, true);
            }
        }

        public async Task InstallDotNetFrameworkAsync()
        {
            // Installs .NET Framework 3.5 (includes 2.0 and 3.0) and 4.x Advanced Services
            const string script = @"
                $ErrorActionPreference = 'SilentlyContinue'
                Write-Host 'Instalando .NET Framework 3.5 (inclui 2.0 e 3.0)...'
                Enable-WindowsOptionalFeature -Online -FeatureName 'NetFx3' -All -NoRestart
                
                Write-Host 'Instalando .NET Framework 4.x Advanced Services...'
                Enable-WindowsOptionalFeature -Online -FeatureName 'NetFx4-AdvSrvs' -All -NoRestart
            ";
            
            await _psService.ExecuteCommandAsync(script);
        }
    }
}
