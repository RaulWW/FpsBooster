using System.Threading.Tasks;

namespace FpsBooster.Services.Optimization
{
    public class AdobeBlockTask : IOptimizationTask
    {
        public string Description => "Otimizando serviços Adobe e bloqueando telemetria";
        public int ProgressWeight => 15;

        public async Task ExecuteAsync(IPowerShellService psService)
        {
            const string adobeScript = @"
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
            await psService.ExecuteCommandAsync(adobeScript);

            const string hostsScript = @"
                $ErrorActionPreference = 'SilentlyContinue'
                $remoteHostsUrl = 'https://raw.githubusercontent.com/Ruddernation-Designs/Adobe-URL-Block-List/master/hosts'
                $localHostsPath = 'C:\Windows\System32\drivers\etc\hosts'
                $tempHostsPath = 'C:\Windows\System32\drivers\etc\temp_hosts'
                
                try {
                    if (Test-Path $localHostsPath) {
                        $localHostsContent = Get-Content $localHostsPath -Raw
                        if ($localHostsContent -notlike '*#AdobeNetBlock-start*') {
                            Write-Host '[INFO] Descarregando lista de bloqueio Adobe...'
                            Invoke-WebRequest -Uri $remoteHostsUrl -OutFile $tempHostsPath -ErrorAction Stop
                            
                            $newBlockContent = Get-Content $tempHostsPath | Where-Object { $_ -notmatch '^\s*#' -and $_ -ne '' }
                            $blockString = ""`n#AdobeNetBlock-start`n"" + ($newBlockContent -join ""`n"") + ""`n#AdobeNetBlock-end""
                            
                            Add-Content -Path $localHostsPath -Value $blockString -Encoding UTF8 -Force
                            Write-Host '[SUCCESS] Hosts atualizado.'
                        }
                    }
                    ipconfig /flushdns | Out-Null
                } catch {
                    Write-Host ""[WARNING] Falha ao atualizar hosts: $($_.Exception.Message)""
                } finally { 
                    if (Test-Path $tempHostsPath) { Remove-Item $tempHostsPath -Force }
                }
            ";
            await psService.ExecuteCommandAsync(hostsScript);
        }
    }
}
