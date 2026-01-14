using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FpsBooster.Services.Optimization;

namespace FpsBooster.Services
{
    public class BoosterService
    {
        private readonly IPowerShellService _psService;
        private readonly List<IOptimizationTask> _optimizationTasks;

        public event Action<string>? OnLogMessage;
        public event Action<int>? OnProgressUpdate;

        public BoosterService(IPowerShellService? psService = null)
        {
            _psService = psService ?? new PowerShellService();
            _psService.OnOutputReceived += (msg) => OnLogMessage?.Invoke(msg);
            _psService.OnErrorReceived += (msg) => OnLogMessage?.Invoke($"[ERROR] {msg}");

            _optimizationTasks = new List<IOptimizationTask>
            {
                new PowerOptimizationTask(),
                new SystemCleanupTask(),
                new RegistryTweaksTask(),
                new ServiceOptimizationTask(),
                new AdobeBlockTask()
            };
        }

        public async Task ApplyUltimatePerformanceAsync()
        {
            int currentProgress = 5;
            OnProgressUpdate?.Invoke(currentProgress);
            OnLogMessage?.Invoke("[INFO] Iniciando otimização EXTREMA do sistema...");

            int totalWeight = 0;
            foreach (var task in _optimizationTasks) totalWeight += task.ProgressWeight;

            foreach (var task in _optimizationTasks)
            {
                OnLogMessage?.Invoke($"[INFO] {task.Description}...");
                await task.ExecuteAsync(_psService);
                
                currentProgress += (int)((task.ProgressWeight / (double)totalWeight) * 90);
                OnProgressUpdate?.Invoke(Math.Min(98, currentProgress));
            }

            OnProgressUpdate?.Invoke(100);
            OnLogMessage?.Invoke("[INFO] SISTEMA OTIMIZADO AO MÁXIMO! Reinicie para aplicar todos os ajustes.");
        }
    }
}
