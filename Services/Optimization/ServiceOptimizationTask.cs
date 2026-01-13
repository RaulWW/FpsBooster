using System.Threading.Tasks;

namespace FpsBooster.Services.Optimization
{
    public class ServiceOptimizationTask : IOptimizationTask
    {
        public string Description => "Otimizando processos svchost e telemetria";
        public int ProgressWeight => 20;

        public async Task ExecuteAsync(IPowerShellService psService)
        {
            const string memoryServicesScript = @"
                $ErrorActionPreference = 'SilentlyContinue'
                # Group svchost.exe processes
                $ram = (Get-CimInstance -ClassName Win32_PhysicalMemory | Measure-Object -Property Capacity -Sum).Sum / 1kb
                Set-ItemProperty -Path 'HKLM:\SYSTEM\CurrentControlSet\Control' -Name 'SvcHostSplitThresholdInKB' -Type DWord -Value $ram -Force

                # Network/Diag Logs
                $autoLoggerDir = Join-Path $env:PROGRAMDATA 'Microsoft\Diagnosis\ETLLogs\AutoLogger'
                if (Test-Path $autoLoggerDir) {
                    Get-ChildItem -Path $autoLoggerDir -Filter '*.etl' | Remove-Item -Force
                    icacls $autoLoggerDir /deny SYSTEM:`(OI`)`(CI`)F | Out-Null
                }

                # Defender Sample Submission
                Set-MpPreference -SubmitSamplesConsent 2
                
                # Edge Management Fix
                if (Test-Path 'HKLM:\SOFTWARE\Policies\Microsoft\Edge') {
                    Remove-Item -Path 'HKLM:\SOFTWARE\Policies\Microsoft\Edge' -Recurse -Force
                }
            ";
            await psService.ExecuteCommandAsync(memoryServicesScript);
        }
    }
}
