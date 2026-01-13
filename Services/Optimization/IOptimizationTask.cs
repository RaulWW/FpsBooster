using System.Threading.Tasks;

namespace FpsBooster.Services.Optimization
{
    public interface IOptimizationTask
    {
        string Description { get; }
        int ProgressWeight { get; }
        Task ExecuteAsync(IPowerShellService psService);
    }
}
