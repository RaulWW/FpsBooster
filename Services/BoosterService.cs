using System;
using System.Threading.Tasks;

namespace FpsBooster.Services
{
    public class BoosterService
    {
        private readonly PowerShellService _psService;

        public event Action<string> OnLogMessage;
        public event Action<int> OnProgressUpdate;

        public BoosterService()
        {
            _psService = new PowerShellService();
            _psService.OnOutputReceived += (msg) => OnLogMessage?.Invoke(msg);
            _psService.OnErrorReceived += (msg) => OnLogMessage?.Invoke($"[ERROR] {msg}");
        }

        public async Task ApplyUltimatePerformanceAsync()
        {
            OnProgressUpdate?.Invoke(5);
            OnLogMessage?.Invoke("Iniciando otimização completa do sistema...");
            
            // 1. Ultimate Performance Power Plan
            OnProgressUpdate?.Invoke(20);
            OnLogMessage?.Invoke("Configurando Plano de Energia: Desempenho Máximo...");
            string powerPlanScript = @"
                $ultimateGuid = 'e9a42b02-d5df-448d-aa00-03f14749eb61'
                $exists = powercfg /list | Select-String $ultimateGuid
                if (-not $exists) {
                    Write-Host 'Importando esquema de Desempenho Máximo...'
                    powercfg -duplicatescheme e9a42b02-d5df-448d-aa00-03f14749eb61
                }
                powercfg -setactive $ultimateGuid
            ";
            await _psService.ExecuteCommandAsync(powerPlanScript);

            // 2. Disable Hibernation
            OnProgressUpdate?.Invoke(40);
            OnLogMessage?.Invoke("Desativando Hibernação para liberar espaço e recursos...");
            await _psService.ExecuteCommandAsync("powercfg.exe /hibernate off");

            // 3. Clear Temp Files
            OnProgressUpdate?.Invoke(60);
            OnLogMessage?.Invoke("Limpando arquivos temporários do sistema e usuário...");
            string cleanupScript = @"
                $ErrorActionPreference = 'SilentlyContinue'
                Get-ChildItem -Path 'C:\Windows\Temp' *.* -Recurse | Remove-Item -Force -Recurse
                Get-ChildItem -Path $env:TEMP *.* -Recurse | Remove-Item -Force -Recurse
                Write-Host 'Arquivos temporários removidos.'
            ";
            await _psService.ExecuteCommandAsync(cleanupScript);

            // 4. Disable Storage Sense
            OnProgressUpdate?.Invoke(80);
            OnLogMessage?.Invoke("Ajustando Registro: Desativando Storage Sense...");
            string registryScript = "Set-ItemProperty -Path 'HKCU:\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\StorageSense\\Parameters\\StoragePolicy' -Name '01' -Value 0 -Type Dword -Force";
            await _psService.ExecuteCommandAsync(registryScript);
            
            OnProgressUpdate?.Invoke(100);
            OnLogMessage?.Invoke("Otimização concluída com sucesso! Sistema pronto para o jogo.");
        }
    }
}
