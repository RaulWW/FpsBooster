using System.Threading.Tasks;

namespace FpsBooster.Services.Optimization
{
    public class SystemCleanupTask : IOptimizationTask
    {
        public string Description => "Limpando arquivos temporários e desativando hibernação";
        public int ProgressWeight => 20;

        public async Task ExecuteAsync(IPowerShellService psService)
        {
            await psService.ExecuteCommandAsync("powercfg.exe /hibernate off");
            
            const string cleanupScript = @"
                $ErrorActionPreference = 'SilentlyContinue'
                Write-Host 'Limpando Windows Temp...'
                Get-ChildItem -Path 'C:\Windows\Temp' *.* -Recurse | Remove-Item -Force -Recurse
                Write-Host 'Limpando User Temp...'
                Get-ChildItem -Path $env:TEMP *.* -Recurse | Remove-Item -Force -Recurse
                Write-Host 'Limpando Prefetch...'
                Get-ChildItem -Path 'C:\Windows\Prefetch' -Recurse | Remove-Item -Force -Recurse
            ";
            await psService.ExecuteCommandAsync(cleanupScript);
        }
    }
}
