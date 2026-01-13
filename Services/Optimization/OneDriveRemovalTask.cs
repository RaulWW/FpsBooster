using System.Threading.Tasks;

namespace FpsBooster.Services.Optimization
{
    public class OneDriveRemovalTask : IOptimizationTask
    {
        public string Description => "Removendo OneDrive e restaurando pastas do sistema";
        public int ProgressWeight => 10;

        public async Task ExecuteAsync(IPowerShellService psService)
        {
            const string oneDriveScript = @"
                $ErrorActionPreference = 'SilentlyContinue'
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
            await psService.ExecuteCommandAsync(oneDriveScript);
        }
    }
}
