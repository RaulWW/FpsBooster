using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace FpsBooster.Services
{
    public class PingResponse
    {
        public IPStatus Status { get; set; }
        public long RoundtripTime { get; set; }
        public int Ttl { get; set; }
    }

    public interface IPingProvider
    {
        Task<PingResponse> SendPingAsync(string host, int timeout);
    }

    public class DefaultPingProvider : IPingProvider
    {
        private readonly Ping _ping = new Ping();
        public async Task<PingResponse> SendPingAsync(string host, int timeout)
        {
            var reply = await _ping.SendPingAsync(host, timeout);
            return new PingResponse
            {
                Status = reply.Status,
                RoundtripTime = reply.RoundtripTime,
                Ttl = reply.Options?.Ttl ?? 0
            };
        }
    }
}
