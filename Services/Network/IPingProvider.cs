using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace FpsBooster.Services.Network
{
    public class PingDiagnosticResponse
    {
        public IPStatus Status { get; set; }
        public long RoundtripTime { get; set; }
        public int TimeToLive { get; set; }
    }

    public interface IPingProvider
    {
        Task<PingDiagnosticResponse> PingHostAsync(string host, int timeout);
    }

    public class DefaultPingProvider : IPingProvider
    {
        private readonly Ping _ping = new Ping();
        
        public async Task<PingDiagnosticResponse> PingHostAsync(string host, int timeout)
        {
            var reply = await _ping.SendPingAsync(host, timeout);
            return new PingDiagnosticResponse
            {
                Status = reply.Status,
                RoundtripTime = reply.RoundtripTime,
                TimeToLive = reply.Options?.Ttl ?? 0
            };
        }
    }
}
