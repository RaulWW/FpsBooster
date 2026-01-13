using System;
using System.Threading.Tasks;

namespace FpsBooster.Services
{
    public interface IPowerShellService
    {
        event Action<string>? OnOutputReceived;
        event Action<string>? OnErrorReceived;
        Task<int> ExecuteCommandAsync(string script);
    }
}
