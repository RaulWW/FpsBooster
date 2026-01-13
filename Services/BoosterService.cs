using System;
using System.Threading.Tasks;

namespace FpsBooster.Services
{
    public class BoosterService
    {
        private readonly IPowerShellService _psService;
 
         public event Action<string>? OnLogMessage;
         public event Action<int>? OnProgressUpdate;
 
         public BoosterService(IPowerShellService? psService = null)
         {
             _psService = psService ?? new PowerShellService();
             _psService.OnOutputReceived += (msg) => OnLogMessage?.Invoke(msg);
             _psService.OnErrorReceived += (msg) => OnLogMessage?.Invoke($"[ERROR] {msg}");
         }

        public async Task ApplyUltimatePerformanceAsync()
        {
            OnProgressUpdate?.Invoke(5);
            OnLogMessage?.Invoke("Iniciando otimização EXTREMA do sistema...");
            
            // 1. Ultimate Performance Power Plan
            OnProgressUpdate?.Invoke(15);
            OnLogMessage?.Invoke("Configurando Plano de Energia: Desempenho Máximo...");
            string powerPlanScript = @"
                $ultimateGuid = 'e9a42b02-d5df-448d-aa00-03f14749eb61'
                $exists = powercfg /list | Select-String $ultimateGuid
                if (-not $exists) {
                    $highPerf = powercfg /list | Select-String 'Extreme' | Select-Object -First 1
                    if ($highPerf) {
                         $highPerfGuid = [regex]::Match($highPerf, '[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}').Value
                         powercfg -setactive $highPerfGuid
                         Write-Host ""Usando Plano de Energia existente: $highPerfGuid""
                         return
                    }
                    powercfg -duplicatescheme e9a42b02-d5df-448d-aa00-03f14749eb61
                }
                powercfg -setactive $ultimateGuid
            ";
            await _psService.ExecuteCommandAsync(powerPlanScript);

            // 2. Performance & Cleanup
            OnProgressUpdate?.Invoke(30);
            OnLogMessage?.Invoke("Desativando Hibernação e limpando arquivos temporários...");
            await _psService.ExecuteCommandAsync("powercfg.exe /hibernate off");
            string cleanupScript = @"
                $ErrorActionPreference = 'SilentlyContinue'
                Write-Host 'Limpando Windows Temp...'
                Get-ChildItem -Path 'C:\Windows\Temp' -Recurse | Remove-Item -Force -Recurse
                Write-Host 'Limpando User Temp...'
                Get-ChildItem -Path $env:TEMP -Recurse | Remove-Item -Force -Recurse
                Write-Host 'Limpando Prefetch...'
                Get-ChildItem -Path 'C:\Windows\Prefetch' -Recurse | Remove-Item -Force -Recurse
            ";
            await _psService.ExecuteCommandAsync(cleanupScript);

            // 3. Registry & BCD Tweaks
            OnProgressUpdate?.Invoke(50);
            OnLogMessage?.Invoke("Aplicando bcdedit e ajustes de registro...");
            string bcdRegistryScript = @"
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
            await _psService.ExecuteCommandAsync(bcdRegistryScript);

            // 4. System Services & Memory
            OnProgressUpdate?.Invoke(70);
            OnLogMessage?.Invoke("Otimizando processos svchost e telemetria...");
            string memoryServicesScript = @"
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
            await _psService.ExecuteCommandAsync(memoryServicesScript);

            // 5. Special TaskMgr Preferences (Conditional)
            OnProgressUpdate?.Invoke(90);
            OnLogMessage?.Invoke("Afinando preferências do Gerenciador de Tarefas...");
            string taskMgrScript = @"
                $ErrorActionPreference = 'SilentlyContinue'
                if ((get-ItemProperty -Path 'HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion' -Name CurrentBuild).CurrentBuild -lt 22557) {
                    $taskmgr = Start-Process -WindowStyle Hidden -FilePath taskmgr.exe -PassThru
                    Start-Sleep -Seconds 1
                    $preferences = Get-ItemProperty -Path 'HKCU:\Software\Microsoft\Windows\CurrentVersion\TaskManager' -Name 'Preferences'
                    if ($preferences) {
                        Stop-Process $taskmgr
                        $preferences.Preferences[28] = 0
                        Set-ItemProperty -Path 'HKCU:\Software\Microsoft\Windows\CurrentVersion\TaskManager' -Name 'Preferences' -Type Binary -Value $preferences.Preferences
                    }
                }
            ";
            await _psService.ExecuteCommandAsync(taskMgrScript);

            // 6. Adobe Performance & Update Block
            OnProgressUpdate?.Invoke(85);
            OnLogMessage?.Invoke("Otimizando serviços Adobe e desativando atualizações...");
            string adobeScript = @"
                $ErrorActionPreference = 'SilentlyContinue'
                # Stop Adobe Desktop Service
                $ccPath = 'C:\Program Files (x86)\Common Files\Adobe\Adobe Desktop Common\ADS\Adobe Desktop Service.exe'
                if (Test-Path $ccPath) {
                    takeown /f $ccPath
                    icacls $ccPath /grant Administrators:F
                    Rename-Item -Path $ccPath -NewName 'Adobe Desktop Service.exe.old' -Force
                }
                
                # Disable Acrobat Updates
                $rootPath = 'HKLM:\SOFTWARE\WOW6432Node\Adobe\Adobe ARM\Legacy\Acrobat'
                $subKeys = Get-ChildItem -Path $rootPath | Where-Object { $_.PSChildName -like '{*}' }
                foreach ($subKey in $subKeys) {
                    $fullPath = Join-Path -Path $rootPath -ChildPath $subKey.PSChildName
                    Set-ItemProperty -Path $fullPath -Name Mode -Value 0
                }
            ";
            await _psService.ExecuteCommandAsync(adobeScript);

            // 7. Adobe Network Block (Hosts)
            OnProgressUpdate?.Invoke(90);
            OnLogMessage?.Invoke("Aplicando bloqueio de telemetria Adobe via HOSTS...");
            string hostsScript = @"
                $remoteHostsUrl = 'https://raw.githubusercontent.com/Ruddernation-Designs/Adobe-URL-Block-List/master/hosts'
                $localHostsPath = 'C:\Windows\System32\drivers\etc\hosts'
                $tempHostsPath = 'C:\Windows\System32\drivers\etc\temp_hosts'
                try {
                    Invoke-WebRequest -Uri $remoteHostsUrl -OutFile $tempHostsPath -ErrorAction Stop
                    $localHostsContent = Get-Content $localHostsPath
                    if ($localHostsContent -notlike '*#AdobeNetBlock-start*') {
                        $newBlockContent = Get-Content $tempHostsPath | Where-Object { $_ -notmatch '^\s*#' -and $_ -ne '' }
                        $combinedContent = $localHostsContent + '#AdobeNetBlock-start' + $newBlockContent + '#AdobeNetBlock-end'
                        $combinedContent | Set-Content $localHostsPath -Encoding ASCII
                    }
                    ipconfig /flushdns
                } finally { Remove-Item $tempHostsPath -ErrorAction Ignore }
            ";
            await _psService.ExecuteCommandAsync(hostsScript);

            // 8. Advanced Tweaks (Copilot & FSO)
            OnProgressUpdate?.Invoke(85);
            OnLogMessage?.Invoke("Otimizando Copilot e Fullscreen (FSO)...");
            string copilotFix = @"
                $ErrorActionPreference = 'SilentlyContinue'
                $package = Get-AppxPackage -AllUsers -Name 'Microsoft.Windows.Copilot'
                if ($package) {
                    Remove-AppxPackage -Package $package.PackageFullName -AllUsers
                    Write-Host 'Copilot removido via Appx'
                } else {
                    $dismPkg = dism /online /get-packages | Select-String 'Microsoft-Windows-UserExperience-Desktop'
                    if ($dismPkg) {
                         $pkgName = ($dismPkg -split ': ')[1]
                         dism /online /remove-package /PackageName:$pkgName /NoRestart
                         Write-Host 'Copilot/Experience removido via DISM'
                    }
                }
            ";
            await _psService.ExecuteCommandAsync(copilotFix);
            await _psService.ExecuteCommandAsync("Set-ItemProperty -Path 'HKCU:\\System\\GameConfigStore' -Name 'GameDVR_DXGIHonorFSEWindowsCompatible' -Value 1 -Type DWord -Force");

            // 9. OneDrive Removal & Shell Fix
            OnProgressUpdate?.Invoke(90);
            OnLogMessage?.Invoke("Removendo OneDrive e restaurando pastas do sistema...");
            string oneDriveScript = @"
                $ErrorActionPreference = 'SilentlyContinue'
                $OneDrivePath = $env:OneDrive
                $regPath = 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Uninstall\OneDriveSetup.exe'
                if (Test-Path $regPath) {
                    $OneDriveUninstallString = Get-ItemPropertyValue $regPath -Name 'UninstallString'
                    $parts = $OneDriveUninstallString.Split(' ')
                    $exe = $parts[0]
                    $args = $parts[1..($parts.Length-1)] -join ' '
                    Start-Process -FilePath $exe -ArgumentList ""$args /silent"" -NoNewWindow -Wait
                }

                # Cleanup Leftovers
                Remove-Item -Recurse -Force ""$env:localappdata\Microsoft\OneDrive""
                Remove-Item -Recurse -Force ""$env:programdata\Microsoft OneDrive""
                if (Test-Path 'HKCU:\Software\Microsoft\OneDrive') {
                    Remove-Item -Path 'HKCU:\Software\Microsoft\OneDrive' -Recurse -Force
                }

                # Explorer Sidebar
                Set-ItemProperty -Path 'HKCR:\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}' -Name 'System.IsPinnedToNameSpaceTree' -Value 0
                Set-ItemProperty -Path 'HKCR:\Wow6432Node\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}' -Name 'System.IsPinnedToNameSpaceTree' -Value 0

                # Shell Folders Restore Default
                $user = $env:userprofile
                $ssf = 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders'
                Set-ItemProperty -Path $ssf -Name 'AppData' -Value ""$user\AppData\Roaming"" -Type ExpandString
                Set-ItemProperty -Path $ssf -Name 'Desktop' -Value ""$user\Desktop"" -Type ExpandString
                Set-ItemProperty -Path $ssf -Name 'Personal' -Value ""$user\Documents"" -Type ExpandString
                Set-ItemProperty -Path $ssf -Name 'My Pictures' -Value ""$user\Pictures"" -Type ExpandString
                Set-ItemProperty -Path $ssf -Name '{374DE290-123F-4565-9164-39C4925E467B}' -Value ""$user\Downloads"" -Type ExpandString
                
                taskkill.exe /F /IM ""explorer.exe""
                Start-Process ""explorer.exe""
            ";
            await _psService.ExecuteCommandAsync(oneDriveScript);

            // 10. Deep Disk Cleanup
            // OnProgressUpdate?.Invoke(98);
            // OnLogMessage?.Invoke("Finalizando com limpeza de disco...");
            // await _psService.ExecuteCommandAsync("cleanmgr.exe /d C: /VERYLOWDISK");
            
            OnProgressUpdate?.Invoke(100);
            OnLogMessage?.Invoke("SISTEMA OTIMIZADO AO MÁXIMO! Reinicie para aplicar todos os ajustes.");
        }
    }
}
