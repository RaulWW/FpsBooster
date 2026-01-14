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
