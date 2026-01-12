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
            OnProgressUpdate?.Invoke(10);
            OnLogMessage?.Invoke("Iniciando configuração de Desempenho Máximo...");
            
            // Check if Ultimate Performance scheme exists
            // Ultimate Performance GUID: e9a42b02-d5df-448d-aa00-03f14749eb61
            string checkScript = "powercfg /list | Select-String 'Ultimate Performance'";
            
            OnProgressUpdate?.Invoke(30);
            OnLogMessage?.Invoke("Verificando planos de energia existentes...");
            
            // Note: If it doesn't exist, we can duplicate the High Performance one or use a known GUID if available
            // PowerShell command to import the ultimate performance scheme if missing
            string setupScript = @"
                $ultimateGuid = 'e9a42b02-d5df-448d-aa00-03f14749eb61'
                $exists = powercfg /list | Select-String $ultimateGuid
                if (-not $exists) {
                    Write-Host 'Importando esquema de Desempenho Máximo...'
                    powercfg -duplicatescheme e9a42b02-d5df-448d-aa00-03f14749eb61
                }
                Write-Host 'Ativando Desempenho Máximo...'
                powercfg -setactive $ultimateGuid
            ";

            await _psService.ExecuteCommandAsync(setupScript);
            
            OnProgressUpdate?.Invoke(100);
            OnLogMessage?.Invoke("Plano de energia 'Desempenho Máximo' aplicado com sucesso!");
        }
    }
}
