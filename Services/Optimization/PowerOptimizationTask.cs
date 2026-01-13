using System.Threading.Tasks;

namespace FpsBooster.Services.Optimization
{
    public class PowerOptimizationTask : IOptimizationTask
    {
        public string Description => "Configurando Plano de Energia: Desempenho Máximo";
        public int ProgressWeight => 15;

        public async Task ExecuteAsync(IPowerShellService psService)
        {
            const string script = @"
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
            await psService.ExecuteCommandAsync(script);
        }
    }
}
