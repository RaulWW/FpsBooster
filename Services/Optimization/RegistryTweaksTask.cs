using System.Threading.Tasks;

namespace FpsBooster.Services.Optimization
{
    public class RegistryTweaksTask : IOptimizationTask
    {
        public string Description => "Aplicando bcdedit e ajustes de registro";
        public int ProgressWeight => 20;

        public async Task ExecuteAsync(IPowerShellService psService)
        {
            const string bcdRegistryScript = @"
                $ErrorActionPreference = 'SilentlyContinue'
                # BCD & Legacy Policy
                bcdedit /set '{current}' bootmenupolicy Legacy | Out-Null
                
                # Storage Sense
                Set-ItemProperty -Path 'HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\StorageSense\Parameters\StoragePolicy' -Name '01' -Value 0 -Type Dword -Force
                
                # Taskbar End Task
                $tbPath = 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\TaskbarDeveloperSettings'
                if (-not (Test-Path $tbPath)) { New-Item -Path $tbPath -Force | Out-Null }
                New-ItemProperty -Path $tbPath -Name 'TaskbarEndTask' -PropertyType Dword -Value 1 -Force | Out-Null

                # Remove Explorer 3D Objects/User items
                Remove-Item -Path 'HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}' -Recurse -Force
            ";
            await psService.ExecuteCommandAsync(bcdRegistryScript);
        }
    }
}
