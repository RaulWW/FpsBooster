using System.Threading.Tasks;

namespace FpsBooster.Services.Optimization
{
    public class TelemetryOptimizationTask : IOptimizationTask
    {
        public string Description => "Desativando Telemetria e Coleta de Dados (Avançado)";
        public int ProgressWeight => 25;

        public async Task ExecuteAsync(IPowerShellService psService)
        {
            const string telemetryScript = @"
                $ErrorActionPreference = 'SilentlyContinue'

                # --- Registry Telemetry Disabling ---
                $regPath = 'HKLM:\SOFTWARE\Policies\Microsoft\Windows\DataCollection'
                if (!(Test-Path $regPath)) { New-Item -Path $regPath -Force | Out-Null }
                Set-ItemProperty -Path $regPath -Name 'AllowTelemetry' -Value 0 -Type DWord -Force

                # Additional Telemetry Keys
                $keys = @(
                    @{Path='HKLM:\SOFTWARE\Policies\Microsoft\Windows\DataCollection'; Name='AllowTelemetry'; Value=0},
                    @{Path='HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection'; Name='AllowTelemetry'; Value=0},
                    @{Path='HKLM:\SOFTWARE\Policies\Microsoft\Windows\CloudContent'; Name='DisableWindowsConsumerFeatures'; Value=1},
                    @{Path='HKLM:\SOFTWARE\Policies\Microsoft\Windows\AdvertisingInfo'; Name='DisabledByGroupPolicy'; Value=1},
                    @{Path='HKCU:\Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo'; Name='Enabled'; Value=0}
                )

                foreach ($k in $keys) {
                    if (!(Test-Path $k.Path)) { New-Item -Path $k.Path -Force | Out-Null }
                    Set-ItemProperty -Path $k.Path -Name $k.Name -Value $k.Value -Type DWord -Force
                }

                # Detailed Registry Tweaks from Request
                $tweaks = @(
                    @{Path='HKLM:\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate'; Name='DoNotConnectToWindowsUpdateInternetLocations'; Value=1},
                    @{Path='HKLM:\SOFTWARE\Policies\Microsoft\Windows\System'; Name='PublishUserActivities'; Value=0},
                    @{Path='HKLM:\SOFTWARE\Policies\Microsoft\Windows\System'; Name='UploadUserActivities'; Value=0}
                )
                 foreach ($t in $tweaks) {
                    if (!(Test-Path $t.Path)) { New-Item -Path $t.Path -Force | Out-Null }
                    Set-ItemProperty -Path $t.Path -Name $t.Name -Value $t.Value -Type DWord -Force
                }


                # --- Disable Scheduled Tasks ---
                $tasks = @(
                    '\Microsoft\Windows\Application Experience\Microsoft Compatibility Appraiser',
                    '\Microsoft\Windows\Application Experience\ProgramDataUpdater',
                    '\Microsoft\Windows\Autochk\Proxy',
                    '\Microsoft\Windows\Customer Experience Improvement Program\Consolidator',
                    '\Microsoft\Windows\Customer Experience Improvement Program\UsbCeip',
                    '\Microsoft\Windows\DiskDiagnostic\Microsoft-Windows-DiskDiagnosticDataCollector',
                    '\Microsoft\Windows\Feedback\Siuf\DmClient',
                    '\Microsoft\Windows\Feedback\Siuf\DmClientOnScenarioDownload',
                    '\Microsoft\Windows\Windows Error Reporting\QueueReporting',
                    '\Microsoft\Windows\Application Experience\MareBackup',
                    '\Microsoft\Windows\Application Experience\StartupAppTask',
                    '\Microsoft\Windows\Application Experience\PcaPatchDbTask',
                    '\Microsoft\Windows\Maps\MapsUpdateTask'
                )

                foreach ($taskName in $tasks) {
                    Disable-ScheduledTask -TaskName $taskName -ErrorAction SilentlyContinue | Out-Null
                }
            ";

            await psService.ExecuteCommandAsync(telemetryScript);
        }
    }
}
