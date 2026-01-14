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
                
                # Close potential locking processes (optional, use with care)
                # Stop-Process -Name 'ccleaner64','ccleaner' -Force -ErrorAction SilentlyContinue

                Write-Host 'Limpando Windows Temp...'
                Get-ChildItem -Path 'C:\Windows\Temp' *.* -Recurse | Remove-Item -Force -Recurse
                
                Write-Host 'Limpando User Temp...'
                Get-ChildItem -Path $env:TEMP *.* -Recurse | Remove-Item -Force -Recurse

                Write-Host 'Limpando Prefetch...'
                Get-ChildItem -Path 'C:\Windows\Prefetch' -Recurse | Remove-Item -Force -Recurse

                Write-Host 'Limpando Event Logs...'
                Get-EventLog -LogName * | ForEach-Object { Clear-EventLog -LogName $_.Log }

                Write-Host 'Limpando Internet Cache...'
                Start-Process -FilePath 'RunDll32.exe' -ArgumentList 'InetCpl.cpl,ClearMyTracksByProcess 8' -Wait -NoNewWindow

                Write-Host 'Limpando Recent Files...'
                $recentPath = Join-Path $env:APPDATA 'Microsoft\Windows\Recent'
                if (Test-Path $recentPath) {
                    Get-ChildItem -Path $recentPath -Recurse | Remove-Item -Force -Recurse
                }

                Write-Host 'Limpando System Logs...'
                $logPaths = @(
                    'C:\Windows\Logs\CBS\*.log',
                    'C:\Windows\Logs\MoSetup\*.log',
                    'C:\Windows\Panther\*.log',
                    'C:\Windows\SoftwareDistribution\*.log',
                    'C:\Windows\Microsoft.NET\*.log'
                )
                foreach ($path in $logPaths) {
                    Get-ChildItem -Path $path -Recurse | Remove-Item -Force -Recurse
                }

                Write-Host 'Limpando User Caches & Logs...'
                $userLogPaths = @(
                    (Join-Path $env:LOCALAPPDATA 'Microsoft\Windows\WebCache\*.log'),
                    (Join-Path $env:LOCALAPPDATA 'Microsoft\Windows\SettingSync\*.log'),
                    (Join-Path $env:LOCALAPPDATA 'Microsoft\Windows\Explorer\ThumbCacheToDelete\*.tmp')
                )
                foreach ($path in $userLogPaths) {
                    Get-ChildItem -Path $path -Recurse | Remove-Item -Force -Recurse
                }
            ";
            await psService.ExecuteCommandAsync(cleanupScript);
        }
    }
}
